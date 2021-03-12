using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000400 RID: 1024
	internal class EhloOptions : IEhloOptions
	{
		// Token: 0x06002EF5 RID: 12021 RVA: 0x000BC108 File Offset: 0x000BA308
		static EhloOptions()
		{
			foreach (object obj in Enum.GetValues(typeof(EhloOptionsFlags)))
			{
				EhloOptionsFlags ehloOptionsFlags = (EhloOptionsFlags)obj;
				EhloOptions.flagStrings[(int)ehloOptionsFlags] = ehloOptionsFlags.ToString();
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x000BC198 File Offset: 0x000BA398
		// (set) Token: 0x06002EF7 RID: 12023 RVA: 0x000BC1A0 File Offset: 0x000BA3A0
		public int Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06002EF8 RID: 12024 RVA: 0x000BC1A9 File Offset: 0x000BA3A9
		// (set) Token: 0x06002EF9 RID: 12025 RVA: 0x000BC1B1 File Offset: 0x000BA3B1
		public string AdvertisedFQDN
		{
			get
			{
				return this.advertisedFQDN;
			}
			set
			{
				this.advertisedFQDN = value;
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06002EFA RID: 12026 RVA: 0x000BC1BA File Offset: 0x000BA3BA
		// (set) Token: 0x06002EFB RID: 12027 RVA: 0x000BC1C2 File Offset: 0x000BA3C2
		public IPAddress AdvertisedIPAddress
		{
			get
			{
				return this.advertisedIPAddress;
			}
			set
			{
				this.advertisedIPAddress = value;
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06002EFC RID: 12028 RVA: 0x000BC1CB File Offset: 0x000BA3CB
		// (set) Token: 0x06002EFD RID: 12029 RVA: 0x000BC1DB File Offset: 0x000BA3DB
		public bool BinaryMime
		{
			get
			{
				return (this.flags & 4) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.BinaryMime, value);
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06002EFE RID: 12030 RVA: 0x000BC1E5 File Offset: 0x000BA3E5
		// (set) Token: 0x06002EFF RID: 12031 RVA: 0x000BC1F6 File Offset: 0x000BA3F6
		public bool EightBitMime
		{
			get
			{
				return (this.flags & 32) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.EightBitMime, value);
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06002F00 RID: 12032 RVA: 0x000BC201 File Offset: 0x000BA401
		// (set) Token: 0x06002F01 RID: 12033 RVA: 0x000BC215 File Offset: 0x000BA415
		public bool StartTLS
		{
			get
			{
				return (this.flags & 256) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.StartTls, value);
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06002F02 RID: 12034 RVA: 0x000BC223 File Offset: 0x000BA423
		// (set) Token: 0x06002F03 RID: 12035 RVA: 0x000BC233 File Offset: 0x000BA433
		public bool AnonymousTLS
		{
			get
			{
				return (this.flags & 1) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.AnonymousTls, value);
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x000BC23D File Offset: 0x000BA43D
		public ICollection<string> ExchangeAuthArgs
		{
			get
			{
				return this.exchangeAuthArgs;
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06002F05 RID: 12037 RVA: 0x000BC245 File Offset: 0x000BA445
		// (set) Token: 0x06002F06 RID: 12038 RVA: 0x000BC259 File Offset: 0x000BA459
		public bool Pipelining
		{
			get
			{
				return (this.flags & 128) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.Pipelining, value);
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06002F07 RID: 12039 RVA: 0x000BC267 File Offset: 0x000BA467
		// (set) Token: 0x06002F08 RID: 12040 RVA: 0x000BC278 File Offset: 0x000BA478
		public bool EnhancedStatusCodes
		{
			get
			{
				return (this.flags & 64) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.EnhancedStatusCodes, value);
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06002F09 RID: 12041 RVA: 0x000BC283 File Offset: 0x000BA483
		// (set) Token: 0x06002F0A RID: 12042 RVA: 0x000BC294 File Offset: 0x000BA494
		public bool Dsn
		{
			get
			{
				return (this.flags & 16) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.Dsn, value);
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000BC29F File Offset: 0x000BA49F
		// (set) Token: 0x06002F0C RID: 12044 RVA: 0x000BC2A7 File Offset: 0x000BA4A7
		public SizeMode Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000BC2B0 File Offset: 0x000BA4B0
		// (set) Token: 0x06002F0E RID: 12046 RVA: 0x000BC2B8 File Offset: 0x000BA4B8
		public long MaxSize
		{
			get
			{
				return this.maxSize;
			}
			set
			{
				this.maxSize = value;
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x000BC2C1 File Offset: 0x000BA4C1
		public ICollection<string> AuthenticationMechanisms
		{
			get
			{
				return this.authenticationMechanisms;
			}
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x000BC2C9 File Offset: 0x000BA4C9
		public void AddAuthenticationMechanism(string mechanism, bool enabled)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("mechanism", mechanism);
			if (enabled && !this.authenticationMechanisms.Contains(mechanism))
			{
				this.authenticationMechanisms.Add(mechanism);
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x000BC2F3 File Offset: 0x000BA4F3
		// (set) Token: 0x06002F12 RID: 12050 RVA: 0x000BC303 File Offset: 0x000BA503
		public bool Chunking
		{
			get
			{
				return (this.flags & 8) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.Chunking, value);
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x000BC30D File Offset: 0x000BA50D
		// (set) Token: 0x06002F14 RID: 12052 RVA: 0x000BC321 File Offset: 0x000BA521
		public bool XMsgId
		{
			get
			{
				return (this.flags & 33554432) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XMsgId, value);
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06002F15 RID: 12053 RVA: 0x000BC32F File Offset: 0x000BA52F
		// (set) Token: 0x06002F16 RID: 12054 RVA: 0x000BC343 File Offset: 0x000BA543
		public bool Xexch50
		{
			get
			{
				return (this.flags & 512) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.Xexch50, value);
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06002F17 RID: 12055 RVA: 0x000BC351 File Offset: 0x000BA551
		// (set) Token: 0x06002F18 RID: 12056 RVA: 0x000BC365 File Offset: 0x000BA565
		public bool XLongAddr
		{
			get
			{
				return (this.flags & 1024) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XlongAddr, value);
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06002F19 RID: 12057 RVA: 0x000BC373 File Offset: 0x000BA573
		// (set) Token: 0x06002F1A RID: 12058 RVA: 0x000BC387 File Offset: 0x000BA587
		public bool XOrar
		{
			get
			{
				return (this.flags & 4096) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.Xorar, value);
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06002F1B RID: 12059 RVA: 0x000BC395 File Offset: 0x000BA595
		// (set) Token: 0x06002F1C RID: 12060 RVA: 0x000BC3A9 File Offset: 0x000BA5A9
		public bool XRDst
		{
			get
			{
				return (this.flags & 16384) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.Xrdst, value);
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x000BC3B7 File Offset: 0x000BA5B7
		// (set) Token: 0x06002F1E RID: 12062 RVA: 0x000BC3CB File Offset: 0x000BA5CB
		public bool XShadow
		{
			get
			{
				return (this.flags & 32768) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.Xshadow, value);
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06002F1F RID: 12063 RVA: 0x000BC3D9 File Offset: 0x000BA5D9
		// (set) Token: 0x06002F20 RID: 12064 RVA: 0x000BC3ED File Offset: 0x000BA5ED
		public bool XShadowRequest
		{
			get
			{
				return (this.flags & 131072) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XshadowRequest, value);
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06002F21 RID: 12065 RVA: 0x000BC3FB File Offset: 0x000BA5FB
		// (set) Token: 0x06002F22 RID: 12066 RVA: 0x000BC40F File Offset: 0x000BA60F
		public bool XOorg
		{
			get
			{
				return (this.flags & 2048) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.Xoorg, value);
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x000BC41D File Offset: 0x000BA61D
		// (set) Token: 0x06002F24 RID: 12068 RVA: 0x000BC431 File Offset: 0x000BA631
		public bool XProxy
		{
			get
			{
				return (this.flags & 8192) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.Xproxy, value);
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x000BC43F File Offset: 0x000BA63F
		// (set) Token: 0x06002F26 RID: 12070 RVA: 0x000BC453 File Offset: 0x000BA653
		public bool XProxyFrom
		{
			get
			{
				return (this.flags & 65536) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XproxyFrom, value);
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x000BC461 File Offset: 0x000BA661
		// (set) Token: 0x06002F28 RID: 12072 RVA: 0x000BC475 File Offset: 0x000BA675
		public bool XAdrc
		{
			get
			{
				return (this.flags & 262144) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XAdrc, value);
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06002F29 RID: 12073 RVA: 0x000BC483 File Offset: 0x000BA683
		// (set) Token: 0x06002F2A RID: 12074 RVA: 0x000BC497 File Offset: 0x000BA697
		public bool XExprops
		{
			get
			{
				return (this.flags & 524288) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XExProps, value);
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x000BC4A5 File Offset: 0x000BA6A5
		public Version XExpropsVersion
		{
			get
			{
				return this.xExpropsVersion;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x000BC4AD File Offset: 0x000BA6AD
		// (set) Token: 0x06002F2D RID: 12077 RVA: 0x000BC4C1 File Offset: 0x000BA6C1
		public bool XFastIndex
		{
			get
			{
				return (this.flags & 8388608) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XFastIndex, value);
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x000BC4CF File Offset: 0x000BA6CF
		// (set) Token: 0x06002F2F RID: 12079 RVA: 0x000BC4E3 File Offset: 0x000BA6E3
		public bool XProxyTo
		{
			get
			{
				return (this.flags & 1048576) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XproxyTo, value);
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x000BC4F1 File Offset: 0x000BA6F1
		// (set) Token: 0x06002F31 RID: 12081 RVA: 0x000BC505 File Offset: 0x000BA705
		public bool XRsetProxyTo
		{
			get
			{
				return (this.flags & 67108864) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XrsetProxyTo, value);
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06002F32 RID: 12082 RVA: 0x000BC513 File Offset: 0x000BA713
		// (set) Token: 0x06002F33 RID: 12083 RVA: 0x000BC527 File Offset: 0x000BA727
		public bool XSessionMdbGuid
		{
			get
			{
				return (this.flags & 2097152) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XSessionMdbGuid, value);
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06002F34 RID: 12084 RVA: 0x000BC535 File Offset: 0x000BA735
		// (set) Token: 0x06002F35 RID: 12085 RVA: 0x000BC549 File Offset: 0x000BA749
		public bool XAttr
		{
			get
			{
				return (this.flags & 4194304) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XAttr, value);
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06002F36 RID: 12086 RVA: 0x000BC557 File Offset: 0x000BA757
		// (set) Token: 0x06002F37 RID: 12087 RVA: 0x000BC56B File Offset: 0x000BA76B
		public bool XSysProbe
		{
			get
			{
				return (this.flags & 16777216) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XSysProbe, value);
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x000BC579 File Offset: 0x000BA779
		// (set) Token: 0x06002F39 RID: 12089 RVA: 0x000BC58D File Offset: 0x000BA78D
		public bool XOrigFrom
		{
			get
			{
				return (this.flags & 268435456) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XOrigFrom, value);
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000BC59B File Offset: 0x000BA79B
		// (set) Token: 0x06002F3B RID: 12091 RVA: 0x000BC5AF File Offset: 0x000BA7AF
		public bool XSessionType
		{
			get
			{
				return (this.flags & 536870912) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.XSessionType, value);
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x000BC5BD File Offset: 0x000BA7BD
		public ICollection<string> ExtendedCommands
		{
			get
			{
				return this.extendedCommands;
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06002F3D RID: 12093 RVA: 0x000BC5C5 File Offset: 0x000BA7C5
		// (set) Token: 0x06002F3E RID: 12094 RVA: 0x000BC5D9 File Offset: 0x000BA7D9
		public bool SmtpUtf8
		{
			get
			{
				return (this.flags & 134217728) != 0;
			}
			set
			{
				this.SetFlags(EhloOptionsFlags.SmtpUtf8, value);
			}
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x000BC5E7 File Offset: 0x000BA7E7
		public bool AreAnyAuthMechanismsSupported()
		{
			return this.AuthenticationMechanisms.Any<string>();
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000BC5F4 File Offset: 0x000BA7F4
		public void SetFlags(EhloOptionsFlags flagsToSet, bool value)
		{
			if (value)
			{
				this.flags |= (int)flagsToSet;
				return;
			}
			this.flags &= (int)(~(int)flagsToSet);
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000BC618 File Offset: 0x000BA818
		public SmtpResponse CreateSmtpResponse(AdrcSmtpMessageContextBlob adrcSmtpMessageContextBlobInstance, ExtendedPropertiesSmtpMessageContextBlob extendedPropertiesSmtpMessageContextBlobInstance, FastIndexSmtpMessageContextBlob fastIndexSmtpMessageContextBlobInstance)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			StringBuilder stringBuilder2 = new StringBuilder(64);
			bool flag = false;
			if (this.AuthenticationMechanisms.Any<string>())
			{
				if (this.AuthenticationMechanisms.Contains("AUTH GSSAPI"))
				{
					stringBuilder.Append(" GSSAPI");
				}
				if (this.AuthenticationMechanisms.Contains("AUTH NTLM"))
				{
					stringBuilder.Append(" NTLM");
				}
				if (this.AuthenticationMechanisms.Contains("AUTH LOGIN"))
				{
					stringBuilder.Append(" LOGIN");
				}
				if (this.AuthenticationMechanisms.Contains("X-EXPS EXCHANGEAUTH"))
				{
					stringBuilder2.Append(" EXCHANGEAUTH");
					flag = true;
				}
				if (this.AuthenticationMechanisms.Contains("X-EXPS GSSAPI"))
				{
					stringBuilder2.Append(" GSSAPI");
				}
				if (this.AuthenticationMechanisms.Contains("X-EXPS NTLM"))
				{
					stringBuilder2.Append(" NTLM");
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Insert(0, "AUTH");
				}
				if (stringBuilder2.Length > 0)
				{
					stringBuilder2.Insert(0, "X-EXPS");
				}
			}
			string text = null;
			if (this.Size == SizeMode.Enabled)
			{
				text = string.Format(CultureInfo.InvariantCulture, "SIZE {0}", new object[]
				{
					this.MaxSize
				});
			}
			else if (this.Size == SizeMode.EnabledWithoutValue)
			{
				text = "SIZE";
			}
			StringBuilder stringBuilder3 = null;
			if (this.XAdrc && adrcSmtpMessageContextBlobInstance.AcceptBlobFromSmptIn)
			{
				stringBuilder3 = new StringBuilder();
				stringBuilder3.Append(AdrcSmtpMessageContextBlob.VersionStringWithSpace);
			}
			if (this.XExprops && extendedPropertiesSmtpMessageContextBlobInstance.AcceptBlobFromSmptIn)
			{
				if (stringBuilder3 == null)
				{
					stringBuilder3 = new StringBuilder();
				}
				stringBuilder3.Append(ExtendedPropertiesSmtpMessageContextBlob.VersionStringWithSpace);
			}
			if (this.XFastIndex && fastIndexSmtpMessageContextBlobInstance.AcceptBlobFromSmptIn)
			{
				if (stringBuilder3 == null)
				{
					stringBuilder3 = new StringBuilder();
				}
				stringBuilder3.Append(FastIndexSmtpMessageContextBlob.VersionStringWithSpace);
			}
			if (stringBuilder3 != null && stringBuilder3.Length > 0)
			{
				stringBuilder3.Insert(0, "X-MESSAGECONTEXT");
			}
			StringBuilder stringBuilder4 = null;
			if (this.XSessionMdbGuid)
			{
				stringBuilder4 = new StringBuilder();
				stringBuilder4.Append("XSESSIONPARAMS MDBGUID");
			}
			if (this.XSessionType)
			{
				stringBuilder4 = (stringBuilder4 ?? new StringBuilder());
				if (stringBuilder4.Length == 0)
				{
					stringBuilder4.Append("XSESSIONPARAMS");
				}
				stringBuilder4.Append(" TYPE");
			}
			StringBuilder stringBuilder5 = null;
			if (this.XProxyTo)
			{
				stringBuilder5 = new StringBuilder();
				stringBuilder5.Append("XPROXYTO");
				if (this.XRsetProxyTo)
				{
					stringBuilder5.AppendFormat(" {0}", "XRSETPROXYTO");
				}
			}
			return new SmtpResponse("250", string.Empty, new string[]
			{
				string.Format(CultureInfo.InvariantCulture, "{0} Hello [{1}]", new object[]
				{
					this.AdvertisedFQDN,
					this.AdvertisedIPAddress
				}),
				text,
				this.Pipelining ? "PIPELINING" : null,
				this.Dsn ? "DSN" : null,
				this.EnhancedStatusCodes ? "ENHANCEDSTATUSCODES" : null,
				this.StartTLS ? "STARTTLS" : null,
				this.AnonymousTLS ? "X-ANONYMOUSTLS" : null,
				(stringBuilder.Length > 0) ? stringBuilder.ToString() : null,
				(stringBuilder2.Length > 0) ? stringBuilder2.ToString() : null,
				flag ? "X-EXCHANGEAUTH SHA256" : null,
				this.EightBitMime ? "8BITMIME" : null,
				this.BinaryMime ? "BINARYMIME" : null,
				this.Chunking ? "CHUNKING" : null,
				this.Xexch50 ? "XEXCH50" : null,
				this.SmtpUtf8 ? "SMTPUTF8" : null,
				this.XLongAddr ? "XLONGADDR" : null,
				this.XOrar ? "XORAR" : null,
				this.XRDst ? "XRDST" : null,
				this.XShadow ? "XSHADOW" : null,
				this.XShadowRequest ? "XSHADOWREQUEST" : null,
				this.XOorg ? "XOORG" : null,
				this.XProxy ? "XPROXY" : null,
				this.XProxyFrom ? "XPROXYFROM" : null,
				(stringBuilder5 != null && stringBuilder5.Length > 0) ? stringBuilder5.ToString() : null,
				(stringBuilder3 != null && stringBuilder3.Length > 0) ? stringBuilder3.ToString() : null,
				(stringBuilder4 != null && stringBuilder4.Length > 0) ? stringBuilder4.ToString() : null,
				this.XAttr ? "XATTR" : null,
				this.XSysProbe ? "XSYSPROBE" : null,
				this.XMsgId ? "XMSGID" : null,
				this.XOrigFrom ? "XORIGFROM" : null
			});
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000BCB3D File Offset: 0x000BAD3D
		public void ParseResponse(SmtpResponse ehloResponse, IPAddress remoteIPAddress)
		{
			this.ParseResponse(ehloResponse, remoteIPAddress, 0);
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000BCB58 File Offset: 0x000BAD58
		public void ParseResponse(SmtpResponse ehloResponse, IPAddress remoteIPAddress, int linesToSkip)
		{
			if (ehloResponse.StatusText == null)
			{
				return;
			}
			string[] statusText = ehloResponse.StatusText;
			int num = statusText.Length;
			if (linesToSkip > num)
			{
				throw new ArgumentException(string.Format("linesToSkip ({0}) cannot be more than total number of lines ({1})", linesToSkip, num));
			}
			for (int i = linesToSkip; i < num; i++)
			{
				bool flag = true;
				string[] advertisements = EhloOptions.GetAdvertisements(statusText[i]);
				if (advertisements != null && advertisements.Length != 0 && advertisements[0].Length != 0)
				{
					char c = advertisements[0][0];
					if (c <= 'S')
					{
						if (c <= 'E')
						{
							if (c != '8')
							{
								switch (c)
								{
								case 'A':
									goto IL_13C;
								case 'B':
									goto IL_1B6;
								case 'C':
									goto IL_191;
								case 'D':
									goto IL_1DB;
								case 'E':
									goto IL_200;
								default:
									goto IL_657;
								}
							}
							else if (advertisements[0].Equals("8bitmime", StringComparison.OrdinalIgnoreCase))
							{
								this.EightBitMime = true;
							}
							else
							{
								flag = false;
							}
						}
						else
						{
							if (c == 'P')
							{
								goto IL_225;
							}
							if (c != 'S')
							{
								goto IL_657;
							}
							goto IL_24A;
						}
					}
					else
					{
						if (c <= 'e')
						{
							if (c != 'X')
							{
								switch (c)
								{
								case 'a':
									goto IL_13C;
								case 'b':
									goto IL_1B6;
								case 'c':
									goto IL_191;
								case 'd':
									goto IL_1DB;
								case 'e':
									goto IL_200;
								default:
									goto IL_657;
								}
							}
						}
						else
						{
							if (c == 'p')
							{
								goto IL_225;
							}
							if (c == 's')
							{
								goto IL_24A;
							}
							if (c != 'x')
							{
								goto IL_657;
							}
						}
						if (advertisements[0].Equals("XEXCH50", StringComparison.OrdinalIgnoreCase))
						{
							this.Xexch50 = true;
						}
						else if (advertisements[0].Equals("XLONGADDR", StringComparison.OrdinalIgnoreCase))
						{
							this.XLongAddr = true;
						}
						else if (advertisements[0].Equals("XORAR", StringComparison.OrdinalIgnoreCase))
						{
							this.XOrar = true;
						}
						else if (advertisements[0].Equals("XRDST", StringComparison.OrdinalIgnoreCase))
						{
							this.XRDst = true;
						}
						else if (advertisements[0].Equals("XSHADOW", StringComparison.OrdinalIgnoreCase))
						{
							this.XShadow = true;
						}
						else if (advertisements[0].Equals("XSHADOWREQUEST", StringComparison.OrdinalIgnoreCase))
						{
							this.XShadowRequest = true;
						}
						else if (advertisements[0].Equals("XOORG", StringComparison.OrdinalIgnoreCase))
						{
							this.XOorg = true;
						}
						else if (advertisements[0].Equals("XPROXY", StringComparison.OrdinalIgnoreCase))
						{
							this.XProxy = true;
						}
						else if (advertisements[0].Equals("XPROXYFROM", StringComparison.OrdinalIgnoreCase))
						{
							this.XProxyFrom = true;
						}
						else if (advertisements[0].Equals("XPROXYTO", StringComparison.OrdinalIgnoreCase))
						{
							this.XProxyTo = true;
							if (advertisements.Length > 1)
							{
								if (Array.Exists<string>(advertisements, (string word) => string.Equals("XRSETPROXYTO", word, StringComparison.OrdinalIgnoreCase)))
								{
									this.XRsetProxyTo = true;
								}
							}
						}
						else if (advertisements[0].Equals("X-EXPS", StringComparison.OrdinalIgnoreCase))
						{
							for (int j = 1; j < advertisements.Length; j++)
							{
								this.AuthenticationMechanisms.Add("X-EXPS " + advertisements[j].ToUpper(CultureInfo.InvariantCulture));
							}
						}
						else if (advertisements[0].Equals("X-MESSAGECONTEXT", StringComparison.OrdinalIgnoreCase))
						{
							for (int k = 1; k < advertisements.Length; k++)
							{
								if (advertisements[k].StartsWith(AdrcSmtpMessageContextBlob.ADRCBlobName, true, CultureInfo.InvariantCulture) && !this.XAdrc)
								{
									Version version;
									this.XAdrc = AdrcSmtpMessageContextBlob.IsSupportedVersion(advertisements[k], false, out version);
								}
								else if (advertisements[k].StartsWith(ExtendedPropertiesSmtpMessageContextBlob.ExtendedPropertiesBlobName, true, CultureInfo.InvariantCulture) && !this.XExprops)
								{
									this.XExprops = ExtendedPropertiesSmtpMessageContextBlob.IsSupportedVersion(advertisements[k], out this.xExpropsVersion);
								}
								else if (advertisements[k].StartsWith(FastIndexSmtpMessageContextBlob.FastIndexBlobName, true, CultureInfo.InvariantCulture) && !this.XFastIndex)
								{
									Version version;
									this.XFastIndex = FastIndexSmtpMessageContextBlob.IsSupportedVersion(advertisements[k], out version);
								}
							}
						}
						else if (advertisements[0].Equals("XSESSIONPARAMS", StringComparison.OrdinalIgnoreCase))
						{
							for (int l = 1; l < advertisements.Length; l++)
							{
								if (advertisements[l].Equals("MDBGUID", StringComparison.OrdinalIgnoreCase))
								{
									this.XSessionMdbGuid = true;
								}
								else if (advertisements[l].Equals("TYPE", StringComparison.OrdinalIgnoreCase))
								{
									this.XSessionType = true;
								}
							}
						}
						else if (advertisements[0].Equals("X-ANONYMOUSTLS", StringComparison.OrdinalIgnoreCase))
						{
							this.AnonymousTLS = true;
						}
						else if (advertisements[0].Equals("X-EXCHANGEAUTH", StringComparison.OrdinalIgnoreCase))
						{
							for (int m = 1; m < advertisements.Length; m++)
							{
								this.exchangeAuthArgs.Add(advertisements[m]);
							}
						}
						else if (advertisements[0].Equals("XATTR", StringComparison.OrdinalIgnoreCase))
						{
							this.XAttr = true;
						}
						else if (advertisements[0].Equals("XSYSPROBE", StringComparison.OrdinalIgnoreCase))
						{
							this.XSysProbe = true;
						}
						else if (advertisements[0].Equals("XMSGID", StringComparison.OrdinalIgnoreCase))
						{
							this.XMsgId = true;
						}
						else if (advertisements[0].Equals("XORIGFROM", StringComparison.OrdinalIgnoreCase))
						{
							this.XOrigFrom = true;
						}
						else
						{
							flag = false;
						}
					}
					IL_65A:
					if (i == linesToSkip && !flag && !this.ParseDomain(advertisements))
					{
						this.AdvertisedFQDN = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
						{
							remoteIPAddress
						});
						goto IL_690;
					}
					goto IL_690;
					IL_13C:
					if (advertisements[0].Equals("AUTH", StringComparison.OrdinalIgnoreCase))
					{
						for (int n = 1; n < advertisements.Length; n++)
						{
							this.AuthenticationMechanisms.Add("AUTH " + advertisements[n].ToUpper(CultureInfo.InvariantCulture));
						}
						goto IL_65A;
					}
					flag = false;
					goto IL_65A;
					IL_191:
					if (advertisements[0].Equals("CHUNKING", StringComparison.OrdinalIgnoreCase))
					{
						this.Chunking = true;
						goto IL_65A;
					}
					flag = false;
					goto IL_65A;
					IL_1B6:
					if (advertisements[0].Equals("BINARYMIME", StringComparison.OrdinalIgnoreCase))
					{
						this.BinaryMime = true;
						goto IL_65A;
					}
					flag = false;
					goto IL_65A;
					IL_1DB:
					if (advertisements[0].Equals("DSN", StringComparison.OrdinalIgnoreCase))
					{
						this.Dsn = true;
						goto IL_65A;
					}
					flag = false;
					goto IL_65A;
					IL_200:
					if (advertisements[0].Equals("ENHANCEDSTATUSCODES", StringComparison.OrdinalIgnoreCase))
					{
						this.EnhancedStatusCodes = true;
						goto IL_65A;
					}
					flag = false;
					goto IL_65A;
					IL_225:
					if (advertisements[0].Equals("PIPELINING", StringComparison.OrdinalIgnoreCase))
					{
						this.Pipelining = true;
						goto IL_65A;
					}
					flag = false;
					goto IL_65A;
					IL_24A:
					if (advertisements[0].Equals("SIZE", StringComparison.OrdinalIgnoreCase))
					{
						this.Size = SizeMode.Enabled;
						if (advertisements.Length >= 2)
						{
							long.TryParse(advertisements[1], out this.maxSize);
							goto IL_65A;
						}
						goto IL_65A;
					}
					else
					{
						if (advertisements[0].Equals("SMTPUTF8", StringComparison.OrdinalIgnoreCase))
						{
							this.SmtpUtf8 = true;
							goto IL_65A;
						}
						if (advertisements[0].Equals("STARTTLS", StringComparison.OrdinalIgnoreCase))
						{
							this.StartTLS = true;
							goto IL_65A;
						}
						flag = false;
						goto IL_65A;
					}
					IL_657:
					flag = false;
					goto IL_65A;
				}
				IL_690:;
			}
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000BD200 File Offset: 0x000BB400
		public void ParseHeloResponse(SmtpResponse heloResponse)
		{
			if (heloResponse.StatusText != null)
			{
				int num = heloResponse.StatusText.Length;
				if (num > 0)
				{
					string[] advertisements = EhloOptions.GetAdvertisements(heloResponse.StatusText[0]);
					if (advertisements != null)
					{
						this.ParseDomain(advertisements);
					}
				}
			}
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000BD240 File Offset: 0x000BB440
		public EhloOptions Clone()
		{
			return new EhloOptions
			{
				advertisedFQDN = this.advertisedFQDN,
				advertisedIPAddress = this.advertisedIPAddress,
				authenticationMechanisms = new List<string>(this.authenticationMechanisms),
				maxSize = this.maxSize,
				size = this.size,
				flags = this.flags,
				extendedCommands = new List<string>(this.extendedCommands),
				exchangeAuthArgs = new List<string>(this.exchangeAuthArgs)
			};
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x000BD2C4 File Offset: 0x000BB4C4
		public bool MatchForInboundProxySession(IEhloOptions other, bool proxyingBdat, out string nonMatchingOptions, out string criticalNonMatchingOptions)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = ~other.Flags;
			int num2 = this.flags & -896772994;
			int num3 = num2 & num;
			if (num3 != 0)
			{
				stringBuilder.Append(EhloOptions.GetFlagsString(num3));
			}
			if (this.maxSize > other.MaxSize && other.MaxSize != 0L)
			{
				stringBuilder.Append("maxSize, ");
			}
			nonMatchingOptions = stringBuilder.ToString();
			int num4 = 21512;
			if (proxyingBdat)
			{
				num4 |= 8;
			}
			int num5 = this.flags & num4;
			int num6 = num5 & num;
			criticalNonMatchingOptions = EhloOptions.GetFlagsString(num6);
			return num6 == 0;
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x000BD35C File Offset: 0x000BB55C
		public bool MatchForClientProxySession(IEhloOptions other, out string nonCriticalNonMatchingOptions, out string criticalNonMatchingOptions)
		{
			int num = ~other.Flags;
			int num2 = this.flags & -830434050 & -5257;
			int num3 = num2 & num;
			int num4 = this.flags & 5256;
			int num5 = num4 & num;
			bool result = num5 == 0;
			StringBuilder stringBuilder = new StringBuilder(EhloOptions.GetFlagsString(num5));
			if (!other.AuthenticationMechanisms.Contains("AUTH LOGIN"))
			{
				result = false;
				EhloOptions.AppendFlagString("AUTH LOGIN", stringBuilder);
			}
			if (this.maxSize > other.MaxSize && other.MaxSize != 0L)
			{
				EhloOptions.AppendFlagString("maxSize", stringBuilder);
			}
			if (this.size != other.Size && (this.size == SizeMode.Enabled || this.size == SizeMode.EnabledWithoutValue) && other.Size == SizeMode.Disabled)
			{
				EhloOptions.AppendFlagString("size", stringBuilder);
			}
			nonCriticalNonMatchingOptions = EhloOptions.GetFlagsString(num3);
			criticalNonMatchingOptions = stringBuilder.ToString();
			return result;
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x000BD43C File Offset: 0x000BB63C
		private static string GetFlagsString(int flags)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			while (flags != 0)
			{
				string arg;
				if ((num & flags) == num && EhloOptions.flagStrings.TryGetValue(num, out arg))
				{
					stringBuilder.AppendFormat("{0}{1}", arg, ", ");
				}
				flags &= ~num;
				num <<= 1;
			}
			int length = ", ".Length;
			if (stringBuilder.Length > length)
			{
				stringBuilder.Remove(stringBuilder.Length - length, length);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000BD4B1 File Offset: 0x000BB6B1
		private static void AppendFlagString(string stringToAppend, StringBuilder sb)
		{
			if (sb.Length > 0)
			{
				sb.Append(", ");
			}
			sb.Append(stringToAppend);
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000BD4D0 File Offset: 0x000BB6D0
		private static string[] GetAdvertisements(string responseLine)
		{
			if (responseLine.Length < 1)
			{
				return null;
			}
			return responseLine.Split(EhloOptions.delimiters, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000BD4E9 File Offset: 0x000BB6E9
		private bool ParseDomain(string[] domainLineWords)
		{
			if (domainLineWords.Length < 1)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "First line of EHLO response must be at least 5 characters long in order to contain the remote server FQDN");
				return false;
			}
			if (!RoutingAddress.IsValidDomain(domainLineWords[0]) && !RoutingAddress.IsDomainIPLiteral(domainLineWords[0]))
			{
				return false;
			}
			this.AdvertisedFQDN = domainLineWords[0];
			return true;
		}

		// Token: 0x04001723 RID: 5923
		public const string AuthKeyword = "AUTH";

		// Token: 0x04001724 RID: 5924
		public const string LoginKeyword = "LOGIN";

		// Token: 0x04001725 RID: 5925
		public const string GssApiKeyword = "GSSAPI";

		// Token: 0x04001726 RID: 5926
		public const string StartTlsKeyword = "STARTTLS";

		// Token: 0x04001727 RID: 5927
		public const string AnonymousTlsKeyword = "X-ANONYMOUSTLS";

		// Token: 0x04001728 RID: 5928
		public const string SessionParamsKeyword = "XSESSIONPARAMS";

		// Token: 0x04001729 RID: 5929
		public const string XLongAddrKeyword = "XLONGADDR";

		// Token: 0x0400172A RID: 5930
		public const string XOrarKeyword = "XORAR";

		// Token: 0x0400172B RID: 5931
		public const string XRDstKeyword = "XRDST";

		// Token: 0x0400172C RID: 5932
		public const string XShadowKeyword = "XSHADOW";

		// Token: 0x0400172D RID: 5933
		public const string XShadowRequestKeyword = "XSHADOWREQUEST";

		// Token: 0x0400172E RID: 5934
		public const string XOorgKeyword = "XOORG";

		// Token: 0x0400172F RID: 5935
		public const string XProxyKeyword = "XPROXY";

		// Token: 0x04001730 RID: 5936
		public const string XProxyFromKeyword = "XPROXYFROM";

		// Token: 0x04001731 RID: 5937
		public const string XExchangeContextKeyword = "X-MESSAGECONTEXT";

		// Token: 0x04001732 RID: 5938
		public const string XProxyToKeyword = "XPROXYTO";

		// Token: 0x04001733 RID: 5939
		public const string XAttrKeyword = "XATTR";

		// Token: 0x04001734 RID: 5940
		public const string XSysProbeKeyword = "XSYSPROBE";

		// Token: 0x04001735 RID: 5941
		public const string XMsgIdKeyword = "XMSGID";

		// Token: 0x04001736 RID: 5942
		public const string XRsetProxyToKeyword = "XRSETPROXYTO";

		// Token: 0x04001737 RID: 5943
		public const string XOrigFromKeyword = "XORIGFROM";

		// Token: 0x04001738 RID: 5944
		private const string FlagsStringSeparator = ", ";

		// Token: 0x04001739 RID: 5945
		private const EhloOptionsFlags clientProxyFlagsToNotCompare = EhloOptionsFlags.AnonymousTls | EhloOptionsFlags.StartTls | EhloOptionsFlags.Xexch50 | EhloOptionsFlags.Xoorg | EhloOptionsFlags.Xproxy | EhloOptionsFlags.Xrdst | EhloOptionsFlags.XproxyFrom | EhloOptionsFlags.XshadowRequest | EhloOptionsFlags.XAdrc | EhloOptionsFlags.XExProps | EhloOptionsFlags.XproxyTo | EhloOptionsFlags.XSessionMdbGuid | EhloOptionsFlags.XAttr | EhloOptionsFlags.XSysProbe | EhloOptionsFlags.XOrigFrom | EhloOptionsFlags.XSessionType;

		// Token: 0x0400173A RID: 5946
		private const EhloOptionsFlags clientProxyCriticalFlagsToCompare = EhloOptionsFlags.Chunking | EhloOptionsFlags.Pipelining | EhloOptionsFlags.XlongAddr | EhloOptionsFlags.Xorar;

		// Token: 0x0400173B RID: 5947
		private const EhloOptionsFlags inboundProxyFlagsToNotCompare = EhloOptionsFlags.AnonymousTls | EhloOptionsFlags.Pipelining | EhloOptionsFlags.StartTls | EhloOptionsFlags.Xexch50 | EhloOptionsFlags.Xoorg | EhloOptionsFlags.Xproxy | EhloOptionsFlags.Xshadow | EhloOptionsFlags.XproxyFrom | EhloOptionsFlags.XshadowRequest | EhloOptionsFlags.XproxyTo | EhloOptionsFlags.XSessionMdbGuid | EhloOptionsFlags.XAttr | EhloOptionsFlags.XSysProbe | EhloOptionsFlags.XrsetProxyTo | EhloOptionsFlags.XOrigFrom | EhloOptionsFlags.XSessionType;

		// Token: 0x0400173C RID: 5948
		private const EhloOptionsFlags inboundProxyCriticalFlagsToCompare = EhloOptionsFlags.Chunking | EhloOptionsFlags.XlongAddr | EhloOptionsFlags.Xorar | EhloOptionsFlags.Xrdst;

		// Token: 0x0400173D RID: 5949
		private static readonly char[] delimiters = new char[]
		{
			' '
		};

		// Token: 0x0400173E RID: 5950
		private static readonly Dictionary<int, string> flagStrings = new Dictionary<int, string>();

		// Token: 0x0400173F RID: 5951
		private string advertisedFQDN;

		// Token: 0x04001740 RID: 5952
		private IPAddress advertisedIPAddress;

		// Token: 0x04001741 RID: 5953
		private SizeMode size;

		// Token: 0x04001742 RID: 5954
		private long maxSize;

		// Token: 0x04001743 RID: 5955
		private List<string> authenticationMechanisms = new List<string>();

		// Token: 0x04001744 RID: 5956
		private int flags;

		// Token: 0x04001745 RID: 5957
		private List<string> extendedCommands = new List<string>();

		// Token: 0x04001746 RID: 5958
		private List<string> exchangeAuthArgs = new List<string>();

		// Token: 0x04001747 RID: 5959
		private Version xExpropsVersion;
	}
}
