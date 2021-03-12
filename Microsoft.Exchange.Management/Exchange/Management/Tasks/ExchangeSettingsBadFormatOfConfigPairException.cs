using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200118B RID: 4491
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsBadFormatOfConfigPairException : ExchangeSettingsException
	{
		// Token: 0x0600B6A8 RID: 46760 RVA: 0x002A038D File Offset: 0x0029E58D
		public ExchangeSettingsBadFormatOfConfigPairException(string pair) : base(Strings.ExchangeSettingsBadFormatOfConfigPair(pair))
		{
			this.pair = pair;
		}

		// Token: 0x0600B6A9 RID: 46761 RVA: 0x002A03A2 File Offset: 0x0029E5A2
		public ExchangeSettingsBadFormatOfConfigPairException(string pair, Exception innerException) : base(Strings.ExchangeSettingsBadFormatOfConfigPair(pair), innerException)
		{
			this.pair = pair;
		}

		// Token: 0x0600B6AA RID: 46762 RVA: 0x002A03B8 File Offset: 0x0029E5B8
		protected ExchangeSettingsBadFormatOfConfigPairException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.pair = (string)info.GetValue("pair", typeof(string));
		}

		// Token: 0x0600B6AB RID: 46763 RVA: 0x002A03E2 File Offset: 0x0029E5E2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("pair", this.pair);
		}

		// Token: 0x17003999 RID: 14745
		// (get) Token: 0x0600B6AC RID: 46764 RVA: 0x002A03FD File Offset: 0x0029E5FD
		public string Pair
		{
			get
			{
				return this.pair;
			}
		}

		// Token: 0x040062FF RID: 25343
		private readonly string pair;
	}
}
