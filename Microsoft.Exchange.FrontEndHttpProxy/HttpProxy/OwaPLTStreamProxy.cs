using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000078 RID: 120
	internal class OwaPLTStreamProxy : StreamProxy
	{
		// Token: 0x060003AC RID: 940 RVA: 0x00015D91 File Offset: 0x00013F91
		internal OwaPLTStreamProxy(StreamProxy.StreamProxyType streamProxyType, Stream source, Stream target, byte[] buffer, IRequestContext requestContext) : base(streamProxyType, source, target, buffer, requestContext)
		{
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00015DA0 File Offset: 0x00013FA0
		protected override byte[] GetUpdatedBufferToSend(ArraySegment<byte> buffer)
		{
			string @string = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
			if (!string.IsNullOrEmpty(@string) && base.RequestContext != null && base.RequestContext.HttpContext.Response != null)
			{
				base.RequestContext.HttpContext.Response.AppendToLog(@string);
			}
			return base.GetUpdatedBufferToSend(buffer);
		}
	}
}
