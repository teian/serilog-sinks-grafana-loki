﻿using Serilog.Sinks.Grafana.Loki.Tests.Fixtures;
using Serilog.Sinks.Grafana.Loki.Tests.Infrastructure;
using Serilog.Sinks.Grafana.Loki.Utils;
using Shouldly;
using Xunit;

namespace Serilog.Sinks.Grafana.Loki.Tests.HttpClientTests
{
    public class RequestsUriTests : IClassFixture<HttpClientTextFixture>
    {
        private readonly TestLokiHttpClient _client;

        public RequestsUriTests()
        {
            _client = new TestLokiHttpClient();
        }

        [Theory]
        [InlineData("http://loki:3100")]
        [InlineData("http://loki:3100/")]
        public void RequestUriShouldBeCorrect(string uri)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.GrafanaLoki("http://loki:3100", httpClient: _client)
                .CreateLogger();

            logger.Error("An error occured");
            logger.Dispose();

            _client.RequestUri.ShouldBe(LokiRoutesBuilder.BuildLogsEntriesRoute(uri));
        }
    }
}