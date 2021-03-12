using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.ClientAccess
{
	// Token: 0x02000028 RID: 40
	internal sealed class UMClientCommonAA : UMClientCommonBase
	{
		// Token: 0x0600025B RID: 603 RVA: 0x00009D44 File Offset: 0x00007F44
		public UMClientCommonAA()
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009D4C File Offset: 0x00007F4C
		public UMClientCommonAA(UMAutoAttendant aa, string userName, string fileName)
		{
			if (aa == null || userName == null || fileName == null)
			{
				throw new ArgumentNullException();
			}
			this.aa = aa;
			this.userToUseForPublishingAAprompt = userName;
			this.fileNameToStoreAARecording = fileName;
			base.TracePrefix = string.Format(CultureInfo.InvariantCulture, "{0}({1}): ", new object[]
			{
				base.GetType().Name,
				this.aa.Name
			});
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009DBC File Offset: 0x00007FBC
		public string PlayOnPhoneAAGreeting(string dialString)
		{
			string result;
			try
			{
				base.DebugTrace("PlayOnPhoneAAGreeting({0})", new object[]
				{
					dialString
				});
				if (UMClientCommonBase.Counters != null)
				{
					UMClientCommonBase.Counters.TotalPlayOnPhoneRequests.Increment();
				}
				this.ValidateAAGreetingParameters();
				UMServerProxy serverByDialplan = UMServerManager.GetServerByDialplan(this.aa.UMDialPlan);
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(this.aa.OrganizationId);
				Guid externalDirectoryOrganizationId = iadsystemConfigurationLookup.GetExternalDirectoryOrganizationId();
				string sessionId = serverByDialplan.PlayOnPhoneAAGreeting(this.aa, externalDirectoryOrganizationId, dialString, this.userToUseForPublishingAAprompt, this.fileNameToStoreAARecording);
				result = base.EncodeCallId(serverByDialplan.Fqdn, sessionId);
			}
			catch (LocalizedException exception)
			{
				base.LogException(exception);
				if (UMClientCommonBase.Counters != null)
				{
					UMClientCommonBase.Counters.TotalPlayOnPhoneErrors.Increment();
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009E8C File Offset: 0x0000808C
		protected override string GetUserContext()
		{
			if (this.aa != null)
			{
				return this.aa.Name;
			}
			return string.Empty;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009EA7 File Offset: 0x000080A7
		protected override void DisposeMe()
		{
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009EA9 File Offset: 0x000080A9
		private void ValidateAAGreetingParameters()
		{
			if (this.aa == null || this.fileNameToStoreAARecording == null || this.userToUseForPublishingAAprompt == null)
			{
				throw new InvalidDataException();
			}
		}

		// Token: 0x040000BB RID: 187
		private string userToUseForPublishingAAprompt;

		// Token: 0x040000BC RID: 188
		private string fileNameToStoreAARecording;

		// Token: 0x040000BD RID: 189
		private UMAutoAttendant aa;
	}
}
