using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001F0 RID: 496
	[Serializable]
	internal class ConfigObjectId : ObjectId
	{
		// Token: 0x0600111F RID: 4383 RVA: 0x00033F6F File Offset: 0x0003216F
		public ConfigObjectId(string identity)
		{
			this.identity = identity;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00033F7E File Offset: 0x0003217E
		public static bool operator ==(ConfigObjectId left, ConfigObjectId right)
		{
			if (left == null)
			{
				return null == right;
			}
			return left.Equals(right);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00033F8F File Offset: 0x0003218F
		public static bool operator !=(ConfigObjectId left, ConfigObjectId right)
		{
			return !(left == right);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00033F9B File Offset: 0x0003219B
		public static explicit operator string(ConfigObjectId id)
		{
			if (null != id)
			{
				return id.identity;
			}
			return null;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00033FAE File Offset: 0x000321AE
		public override bool Equals(object other)
		{
			return other != null && other is ConfigObjectId && this.identity == ((ConfigObjectId)other).identity;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00033FD3 File Offset: 0x000321D3
		public override int GetHashCode()
		{
			if (this.identity != null)
			{
				return this.identity.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00033FEA File Offset: 0x000321EA
		public override string ToString()
		{
			return this.identity;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00033FF2 File Offset: 0x000321F2
		public override byte[] GetBytes()
		{
			if (this.identity != null)
			{
				return Encoding.Unicode.GetBytes(this.identity);
			}
			return null;
		}

		// Token: 0x04000A92 RID: 2706
		private string identity;
	}
}
