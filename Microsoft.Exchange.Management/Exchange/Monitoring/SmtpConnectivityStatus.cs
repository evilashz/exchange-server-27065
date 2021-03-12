using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005C6 RID: 1478
	[Serializable]
	public class SmtpConnectivityStatus : ConfigurableObject
	{
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x060033C8 RID: 13256 RVA: 0x000D1CA3 File Offset: 0x000CFEA3
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return SmtpConnectivityStatus.schema;
			}
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000D1CAC File Offset: 0x000CFEAC
		public SmtpConnectivityStatus(Server server, ReceiveConnector receiveConnector, IPBinding binding, IPEndPoint endPoint) : base(new SimpleProviderPropertyBag())
		{
			string text = binding.ToString();
			string text2 = endPoint.ToString();
			string identity = string.Format("{0}\\{1}\\{2}", new object[]
			{
				receiveConnector.Identity,
				text,
				text2,
				CultureInfo.CurrentUICulture
			});
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = new ConfigObjectId(identity);
			this.Server = server.Name;
			this.ReceiveConnector = receiveConnector.Name;
			this.Binding = text;
			this.EndPoint = text2;
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x060033CA RID: 13258 RVA: 0x000D1D3C File Offset: 0x000CFF3C
		// (set) Token: 0x060033CB RID: 13259 RVA: 0x000D1D4E File Offset: 0x000CFF4E
		public string Server
		{
			get
			{
				return (string)this[SmtpConnectivityStatusSchema.Server];
			}
			private set
			{
				this[SmtpConnectivityStatusSchema.Server] = value;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x000D1D5C File Offset: 0x000CFF5C
		// (set) Token: 0x060033CD RID: 13261 RVA: 0x000D1D6E File Offset: 0x000CFF6E
		public string ReceiveConnector
		{
			get
			{
				return (string)this[SmtpConnectivityStatusSchema.ReceiveConnector];
			}
			private set
			{
				this[SmtpConnectivityStatusSchema.ReceiveConnector] = value;
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x000D1D7C File Offset: 0x000CFF7C
		// (set) Token: 0x060033CF RID: 13263 RVA: 0x000D1D8E File Offset: 0x000CFF8E
		public string Binding
		{
			get
			{
				return (string)this[SmtpConnectivityStatusSchema.Binding];
			}
			private set
			{
				this[SmtpConnectivityStatusSchema.Binding] = value;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x060033D0 RID: 13264 RVA: 0x000D1D9C File Offset: 0x000CFF9C
		// (set) Token: 0x060033D1 RID: 13265 RVA: 0x000D1DAE File Offset: 0x000CFFAE
		public string EndPoint
		{
			get
			{
				return (string)this[SmtpConnectivityStatusSchema.EndPoint];
			}
			private set
			{
				this[SmtpConnectivityStatusSchema.EndPoint] = value;
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x060033D2 RID: 13266 RVA: 0x000D1DBC File Offset: 0x000CFFBC
		// (set) Token: 0x060033D3 RID: 13267 RVA: 0x000D1DCE File Offset: 0x000CFFCE
		public SmtpConnectivityStatusCode StatusCode
		{
			get
			{
				return (SmtpConnectivityStatusCode)this[SmtpConnectivityStatusSchema.StatusCode];
			}
			internal set
			{
				this[SmtpConnectivityStatusSchema.StatusCode] = value;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x060033D4 RID: 13268 RVA: 0x000D1DE1 File Offset: 0x000CFFE1
		// (set) Token: 0x060033D5 RID: 13269 RVA: 0x000D1DF3 File Offset: 0x000CFFF3
		public string Details
		{
			get
			{
				return (string)this[SmtpConnectivityStatusSchema.Details];
			}
			internal set
			{
				this[SmtpConnectivityStatusSchema.Details] = value;
			}
		}

		// Token: 0x040023F0 RID: 9200
		private static SmtpConnectivityStatusSchema schema = ObjectSchema.GetInstance<SmtpConnectivityStatusSchema>();
	}
}
