using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200001C RID: 28
	internal class VersionedDomainAnchorMailbox : DomainAnchorMailbox
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00005506 File Offset: 0x00003706
		public VersionedDomainAnchorMailbox(string domain, int version, IRequestContext requestContext) : base(AnchorSource.DomainAndVersion, domain + "~" + version.ToString(), requestContext)
		{
			this.domain = domain;
			this.Version = version;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005530 File Offset: 0x00003730
		public override string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005538 File Offset: 0x00003738
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00005540 File Offset: 0x00003740
		public int Version { get; private set; }

		// Token: 0x060000C9 RID: 201 RVA: 0x0000554C File Offset: 0x0000374C
		public static AnchorMailbox GetAnchorMailbox(string domain, string versionString, IRequestContext requestContext)
		{
			ServerVersion serverVersion = VersionedDomainAnchorMailbox.ParseServerVersion(versionString);
			if (serverVersion == null)
			{
				return new DomainAnchorMailbox(domain, requestContext);
			}
			return new VersionedDomainAnchorMailbox(domain, serverVersion.Major, requestContext);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005594 File Offset: 0x00003794
		protected override ADRawEntry LoadADRawEntry()
		{
			if (this.Version >= 15)
			{
				return base.LoadADRawEntry();
			}
			IRecipientSession session = base.GetDomainRecipientSession();
			ADRawEntry ret = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => HttpProxyBackEndHelper.GetE14EDiscoveryMailbox(session));
			return base.CheckForNullAndThrowIfApplicable<ADRawEntry>(ret);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000055E8 File Offset: 0x000037E8
		private static ServerVersion ParseServerVersion(string versionString)
		{
			ServerVersion result = null;
			if (!string.IsNullOrEmpty(versionString))
			{
				Match match = Constants.ExchClientVerRegex.Match(versionString);
				ServerVersion serverVersion;
				if (match.Success && RegexUtilities.TryGetServerVersionFromRegexMatch(match, out serverVersion) && serverVersion.Major >= 14)
				{
					result = serverVersion;
				}
			}
			return result;
		}

		// Token: 0x0400004D RID: 77
		private readonly string domain;
	}
}
