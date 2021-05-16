# Sanity C# Client

* Block Content tools
* HTTP API wrapper

# Benchmarks

Here's some benchmark results when trying to parse a huge PortableText. Using raw JSON API is almost double as fast, but it's so fast that it doesn't really matter. One would usually output cache the results of HTML anyways.

Benchmark results when using raw JSON API:

|   Method |           Job |       Runtime |     Mean |     Error |    StdDev | Ratio |
|--------- |-------------- |-------------- |---------:|----------:|----------:|------:|
| HugeJson |    .NET 4.7.2 |    .NET 4.7.2 | 1.932 ms | 0.0084 ms | 0.0079 ms |  1.00 |
| HugeJson | .NET Core 3.0 | .NET Core 3.0 | 1.410 ms | 0.0234 ms | 0.0207 ms |  0.73 |

---

Benchmark results when using deserialization:

|   Method |           Job |       Runtime |     Mean |     Error |    StdDev | Ratio |
|--------- |-------------- |-------------- |---------:|----------:|----------:|------:|
| HugeJson |    .NET 4.7.2 |    .NET 4.7.2 | 3.771 ms | 0.0669 ms | 0.0593 ms |  1.00 |
| HugeJson | .NET Core 3.0 | .NET Core 3.0 | 2.666 ms | 0.0473 ms | 0.0419 ms |  0.71 |
