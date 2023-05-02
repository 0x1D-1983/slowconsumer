using Akka.Actor;
using Akka.Streams;
using Akka.Streams.Dsl;

var system = ActorSystem.Create("SlowConsumerSystem");
var materializer = system.Materializer();

// fast upstream producer - 10,000 events/s
var source = Source.From(Enumerable.Range(0, 10_000));

// group into batches of 100
var batching = source.Via(Flow.Create<int>().Grouped(100));

var start = DateTime.UtcNow;

// slow downstream consumer - 10 events/s
var sink = batching.Via(Flow.Create<IEnumerable<int>>()
    .Delay(TimeSpan.FromMilliseconds(1000), DelayOverflowStrategy.Backpressure)
    .Select(x => (x.Sum(), DateTime.UtcNow - start))); // sum each group

var output = sink.RunAsAsyncEnumerable(materializer);

await foreach(var x in output)
{
    Console.WriteLine(x);
}