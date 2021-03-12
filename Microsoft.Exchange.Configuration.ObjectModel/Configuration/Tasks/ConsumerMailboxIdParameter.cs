using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000164 RID: 356
	[Serializable]
	public class ConsumerMailboxIdParameter : ADIdParameter
	{
		// Token: 0x06000CCB RID: 3275 RVA: 0x00027D93 File Offset: 0x00025F93
		public ConsumerMailboxIdParameter()
		{
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00027D9B File Offset: 0x00025F9B
		public ConsumerMailboxIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00027DA4 File Offset: 0x00025FA4
		protected ConsumerMailboxIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00027DAD File Offset: 0x00025FAD
		public ConsumerMailboxIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00027DB6 File Offset: 0x00025FB6
		public static ConsumerMailboxIdParameter Parse(string identity)
		{
			return new ConsumerMailboxIdParameter(identity);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00027DC0 File Offset: 0x00025FC0
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			notFoundReason = null;
			if (typeof(T) != typeof(ADUser))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			List<ADUser> list = new List<ADUser>();
			ADUser aduser = null;
			Guid exchangeGuid;
			WindowsLiveId windowsLiveId;
			if (Guid.TryParse(base.RawIdentity, out exchangeGuid))
			{
				aduser = ((IRecipientSession)session).FindByExchangeGuidIncludingAlternate<ADUser>(exchangeGuid);
			}
			else if (ConsumerMailboxIdParameter.TryParseWindowsLiveId(base.RawIdentity, out windowsLiveId))
			{
				if (windowsLiveId.NetId != null)
				{
					aduser = ((IRecipientSession)session).FindByExchangeGuidIncludingAlternate<ADUser>(ConsumerIdentityHelper.GetExchangeGuidFromPuid(windowsLiveId.NetId.ToUInt64()));
				}
				else
				{
					aduser = ((IRecipientSession)session).FindByProxyAddress<ADUser>(new SmtpProxyAddress(windowsLiveId.SmtpAddress.ToString(), true));
				}
			}
			else if (base.InternalADObjectId != null)
			{
				aduser = ((IRecipientSession)session).FindADUserByObjectId(base.InternalADObjectId);
			}
			if (aduser != null)
			{
				list.Add(aduser);
			}
			return list as IEnumerable<T>;
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00027EC8 File Offset: 0x000260C8
		private static bool TryParseWindowsLiveId(string identity, out WindowsLiveId liveId)
		{
			liveId = null;
			if (!identity.Contains("\\"))
			{
				return WindowsLiveId.TryParse(identity, out liveId);
			}
			int num = identity.LastIndexOf('\\');
			return WindowsLiveId.TryParse(identity.Substring(num + 1), out liveId);
		}
	}
}
