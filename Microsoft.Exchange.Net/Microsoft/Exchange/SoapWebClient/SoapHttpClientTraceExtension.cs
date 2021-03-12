using System;
using System.IO;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020006DD RID: 1757
	internal sealed class SoapHttpClientTraceExtension : SoapExtension
	{
		// Token: 0x060020DE RID: 8414 RVA: 0x0004128F File Offset: 0x0003F48F
		public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
		{
			return null;
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x00041292 File Offset: 0x0003F492
		public override object GetInitializer(Type WebServiceType)
		{
			return null;
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x00041295 File Offset: 0x0003F495
		public override void Initialize(object initializer)
		{
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x00041297 File Offset: 0x0003F497
		public override Stream ChainStream(Stream stream)
		{
			if (SoapHttpClientTraceExtension.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				this.oldStream = stream;
				this.newStream = new MemoryStream();
				this.reader = new StreamReader(this.newStream);
				return this.newStream;
			}
			return stream;
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000412D4 File Offset: 0x0003F4D4
		public override void ProcessMessage(SoapMessage message)
		{
			if (this.oldStream != null && this.newStream != null)
			{
				switch (message.Stage)
				{
				case SoapMessageStage.AfterSerialize:
					this.WriteRequest();
					return;
				case (SoapMessageStage)3:
					break;
				case SoapMessageStage.BeforeDeserialize:
					this.ReadResponse();
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x0004131C File Offset: 0x0003F51C
		private void WriteRequest()
		{
			this.newStream.Position = 0L;
			string arg = this.reader.ReadToEnd();
			this.newStream.Position = 0L;
			if (this.oldStream.CanWrite)
			{
				try
				{
					SoapHttpClientTraceExtension.Copy(this.newStream, this.oldStream);
				}
				catch (NotSupportedException)
				{
				}
			}
			SoapHttpClientTraceExtension.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Request: {0}", arg);
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x0004139C File Offset: 0x0003F59C
		private void ReadResponse()
		{
			string arg = "<stream not readable>";
			if (this.oldStream.CanRead)
			{
				try
				{
					SoapHttpClientTraceExtension.Copy(this.oldStream, this.newStream);
					this.newStream.Position = 0L;
					arg = this.reader.ReadToEnd();
					this.newStream.Position = 0L;
				}
				catch (NotSupportedException)
				{
				}
			}
			SoapHttpClientTraceExtension.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Response: {0}", arg);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x00041420 File Offset: 0x0003F620
		private static void Copy(Stream from, Stream to)
		{
			byte[] array = new byte[1024];
			for (;;)
			{
				int num = from.Read(array, 0, array.Length);
				if (num == 0)
				{
					break;
				}
				to.Write(array, 0, num);
			}
		}

		// Token: 0x04001F74 RID: 8052
		private Stream oldStream;

		// Token: 0x04001F75 RID: 8053
		private Stream newStream;

		// Token: 0x04001F76 RID: 8054
		private StreamReader reader;

		// Token: 0x04001F77 RID: 8055
		private static Trace Tracer = ExTraceGlobals.EwsClientTracer;
	}
}
