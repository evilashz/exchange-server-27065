using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000447 RID: 1095
	internal class Exch50Writer
	{
		// Token: 0x06003283 RID: 12931 RVA: 0x000C5C2A File Offset: 0x000C3E2A
		public Exch50Writer()
		{
			this.buffer = new List<byte>();
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000C5C40 File Offset: 0x000C3E40
		public void Add(MdbefPropertyCollection properties)
		{
			if (properties.Count == 0)
			{
				this.buffer.AddRange(Exch50Writer.Placeholder);
				return;
			}
			byte[] bytes = properties.GetBytes();
			this.buffer.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(bytes.Length)));
			this.buffer.AddRange(bytes);
			while (this.buffer.Count % 4 != 0)
			{
				this.buffer.Add(0);
			}
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000C5CAE File Offset: 0x000C3EAE
		public byte[] GetBytes()
		{
			return this.buffer.ToArray();
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000C5CBC File Offset: 0x000C3EBC
		// Note: this type is marked as 'beforefieldinit'.
		static Exch50Writer()
		{
			byte[] placeholder = new byte[4];
			Exch50Writer.Placeholder = placeholder;
		}

		// Token: 0x0400199F RID: 6559
		private static readonly byte[] Placeholder;

		// Token: 0x040019A0 RID: 6560
		private List<byte> buffer;
	}
}
