# Akka.Streams Slow Consumer simulator

Reactive Streams application to simulate a scenario where we have a slow downstream consumer with fast upstream producer.
Akka.Streams is used to solve the problem with a  non-blocking backpressure system that allows the consumer to tell the producer to back-off.

## Source

Fast producer instantly generates numbers from 1 to 10,000. Group these numbers into batches of 100 (first batch containes the numbers: 0..99)

## Sink

The IAsyncEnumerable that we materialize the stream into can buffer up to 16 messages at any given time (internal implementation detail, but it’s configurable). Each time the 16 message threshold is reached, the Flow.Delay kicks in and backpressures the upstream for up to 1 second.

## Output

This means in the first second we gonna process 16 events, that is 16 batches of 100 messages. The LINQ style data shaping flow calculates the sum of the numbers in the batches this way the first line contains the sum of the numbers 0..99 = 4950

Tip: The formula to calculate the Sum of the first N numbers is: N*(N+1)/2

`(4950, 00:00:01.0523240)
(14950, 00:00:01.0631750)
(24950, 00:00:01.0631940)
(34950, 00:00:01.0632000)
(44950, 00:00:01.0632040)
(54950, 00:00:01.0632060)
(64950, 00:00:01.0632130)
(74950, 00:00:01.0632160)
(84950, 00:00:01.0632190)
(94950, 00:00:01.0632240)
(104950, 00:00:01.0632270)
(114950, 00:00:01.0632300)
(124950, 00:00:01.0632330)
(134950, 00:00:01.0632430)
(144950, 00:00:01.0632480)
(154950, 00:00:01.0632540)
(164950, 00:00:02.0659210)
(174950, 00:00:02.0662830)
(184950, 00:00:02.0662950)
(194950, 00:00:02.0663040)
(204950, 00:00:02.0663130)
(214950, 00:00:02.0663370)
(224950, 00:00:02.0663470)
(234950, 00:00:02.0663560)
(244950, 00:00:02.0663650)
(254950, 00:00:02.0663740)
(264950, 00:00:02.0663910)
(274950, 00:00:02.0663990)
(284950, 00:00:02.0671030)
(294950, 00:00:02.0671230)
(304950, 00:00:02.0671460)
(314950, 00:00:02.0671560)`