using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x0200005D RID: 93
	internal class UcmaSipUri : PlatformSipUri
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x000125EC File Offset: 0x000107EC
		public UcmaSipUri(string uri)
		{
			this.impl = new SipUriParser(uri);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00012600 File Offset: 0x00010800
		public UcmaSipUri(SipUriScheme scheme, string user, string host)
		{
			this.impl = (string.IsNullOrEmpty(user) ? new SipUriParser(scheme.ToString().ToLowerInvariant(), host) : new SipUriParser(scheme.ToString().ToLowerInvariant(), user, host));
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00012650 File Offset: 0x00010850
		private UcmaSipUri(SipUriParser impl)
		{
			this.impl = impl;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0001265F File Offset: 0x0001085F
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0001266C File Offset: 0x0001086C
		public override string Host
		{
			get
			{
				return this.impl.Host;
			}
			set
			{
				this.impl.Host = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0001267A File Offset: 0x0001087A
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00012687 File Offset: 0x00010887
		public override int Port
		{
			get
			{
				return this.impl.Port;
			}
			set
			{
				this.impl.Port = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00012695 File Offset: 0x00010895
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x000126A2 File Offset: 0x000108A2
		public override string User
		{
			get
			{
				return this.impl.User;
			}
			set
			{
				this.impl.User = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x000126B0 File Offset: 0x000108B0
		public override string SimplifiedUri
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}@{1}", new object[]
				{
					this.User,
					this.Host
				});
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x000126E6 File Offset: 0x000108E6
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x00012717 File Offset: 0x00010917
		public override UserParameter UserParameter
		{
			get
			{
				if (this.impl.UserParameter == null)
				{
					return UserParameter.None;
				}
				return (UserParameter)Enum.Parse(typeof(UserParameter), this.impl.UserParameter, true);
			}
			set
			{
				if (value == UserParameter.None)
				{
					this.impl.UserParameter = null;
					return;
				}
				this.impl.UserParameter = value.ToString().ToLowerInvariant();
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00012744 File Offset: 0x00010944
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x00012775 File Offset: 0x00010975
		public override TransportParameter TransportParameter
		{
			get
			{
				if (this.impl.TransportParameter == null)
				{
					return TransportParameter.None;
				}
				return (TransportParameter)Enum.Parse(typeof(TransportParameter), this.impl.TransportParameter, true);
			}
			set
			{
				if (value == TransportParameter.None)
				{
					this.impl.TransportParameter = null;
					return;
				}
				this.impl.TransportParameter = value.ToString().ToLowerInvariant();
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000127A4 File Offset: 0x000109A4
		public static bool TryParse(string uriString, out PlatformSipUri sipUri)
		{
			SipUriParser sipUriParser = null;
			sipUri = null;
			if (SipUriParser.TryParse(uriString, ref sipUriParser))
			{
				sipUri = new UcmaSipUri(sipUriParser);
			}
			return null != sipUri;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000127D0 File Offset: 0x000109D0
		public override void AddParameter(string name, string value)
		{
			SipUriParameter sipUriParameter = new SipUriParameter(name, value);
			this.impl.AddParameter(sipUriParameter);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000127F4 File Offset: 0x000109F4
		public override string FindParameter(string name)
		{
			string result = null;
			SipUriParameter sipUriParameter = this.impl.FindParameter(name);
			if (sipUriParameter != null)
			{
				result = sipUriParameter.Value;
			}
			return result;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001281C File Offset: 0x00010A1C
		public override void RemoveParameter(string name)
		{
			SipUriParameter sipUriParameter = this.impl.FindParameter(name);
			if (sipUriParameter != null)
			{
				this.impl.RemoveParameter(sipUriParameter);
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00012848 File Offset: 0x00010A48
		public override IEnumerable<PlatformSipUriParameter> GetParametersThatHaveValues()
		{
			List<PlatformSipUriParameter> list = new List<PlatformSipUriParameter>(8);
			foreach (SipUriParameter sipUriParameter in this.impl.GetParameters())
			{
				if (sipUriParameter != null && sipUriParameter.IsSet)
				{
					list.Add(new PlatformSipUriParameter(sipUriParameter.Name, sipUriParameter.Value));
				}
			}
			return list;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000128C0 File Offset: 0x00010AC0
		public override string ToString()
		{
			return this.impl.ToString();
		}

		// Token: 0x04000140 RID: 320
		private SipUriParser impl;
	}
}
