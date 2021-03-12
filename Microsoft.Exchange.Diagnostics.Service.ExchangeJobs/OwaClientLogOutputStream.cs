using System;
using System.IO;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs
{
	// Token: 0x0200002E RID: 46
	public sealed class OwaClientLogOutputStream : FileChunkOutputStream
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00008E17 File Offset: 0x00007017
		public OwaClientLogOutputStream(OutputStream stream) : this(stream, Environment.MachineName + "_OWAClientLogOutputStream", Path.Combine(new JobConfiguration("default").DiagnosticsRootDirectory, "CosmosLog"), OwaClientLogOutputStream.DefaultFields)
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00008E4D File Offset: 0x0000704D
		public OwaClientLogOutputStream(OutputStream stream, string name, string outputDirectory, string[] fields) : base(name, outputDirectory, fields)
		{
			this.defaultOutputStream = stream;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008E60 File Offset: 0x00007060
		public override OutputStream OpenOutputStream(string analyzerName, string outputFormatName, string streamName)
		{
			if (!string.Equals(streamName, "OWARawUserLatencyData", StringComparison.OrdinalIgnoreCase))
			{
				return ((FileChunkOutputStream)this.defaultOutputStream).OpenOutputStream(analyzerName, outputFormatName, streamName);
			}
			return new OwaClientLogOutputStream.WrappedOwaClientLogOutputStream(this, analyzerName, outputFormatName);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00008E8C File Offset: 0x0000708C
		protected override void InternalWriteHeaderLine(string format, params object[] args)
		{
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008E8E File Offset: 0x0000708E
		protected override void InternalWriteLine(string format, params object[] args)
		{
			base.WriteRawLine(format, args);
		}

		// Token: 0x04000135 RID: 309
		private const string OwaRawUserDataOutputFile = "OWARawUserLatencyData";

		// Token: 0x04000136 RID: 310
		private static readonly string[] DefaultFields = new string[]
		{
			"DateTime",
			"MailboxGuid",
			"Country_ISP",
			"TenantGuid",
			"Country_State",
			"ConnectTime"
		};

		// Token: 0x04000137 RID: 311
		private readonly OutputStream defaultOutputStream;

		// Token: 0x0200002F RID: 47
		private class WrappedOwaClientLogOutputStream : OutputStream
		{
			// Token: 0x0600010A RID: 266 RVA: 0x00008EE2 File Offset: 0x000070E2
			public WrappedOwaClientLogOutputStream(OwaClientLogOutputStream stream, string analyzerName, string outputFormat) : base(analyzerName)
			{
				this.stream = stream;
			}

			// Token: 0x0600010B RID: 267 RVA: 0x00008EF2 File Offset: 0x000070F2
			protected override void InternalDispose(bool disposing)
			{
			}

			// Token: 0x0600010C RID: 268 RVA: 0x00008EF4 File Offset: 0x000070F4
			protected override void InternalWriteHeaderLine(string format, params object[] args)
			{
			}

			// Token: 0x0600010D RID: 269 RVA: 0x00008EF6 File Offset: 0x000070F6
			protected override void InternalWriteLine(string format, params object[] args)
			{
				this.stream.WriteRawLine(format, args);
			}

			// Token: 0x04000138 RID: 312
			private readonly OwaClientLogOutputStream stream;
		}
	}
}
