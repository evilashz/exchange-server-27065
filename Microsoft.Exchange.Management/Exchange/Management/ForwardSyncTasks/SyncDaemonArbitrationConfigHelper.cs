using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200037D RID: 893
	internal static class SyncDaemonArbitrationConfigHelper
	{
		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0008744F File Offset: 0x0008564F
		internal static string SyncDaemonLeaseShare
		{
			get
			{
				return "SyncDaemonLeaseShare";
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x00087456 File Offset: 0x00085656
		internal static string ServerNameForFakeLock
		{
			get
			{
				return "Fake-Server";
			}
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x00087460 File Offset: 0x00085660
		internal static ArbitrationConfigFromAD GetArbitrationConfigFromAD(string serviceInstanceName)
		{
			IConfigurationSession configurationSession = ForwardSyncDataAccessHelper.CreateSession(true);
			RidMasterInfo ridMasterInfo = SyncDaemonArbitrationConfigHelper.GetRidMasterInfo(configurationSession);
			SyncServiceInstance[] array = configurationSession.Find<SyncServiceInstance>(SyncServiceInstance.GetMsoSyncRootContainer(), QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, serviceInstanceName), null, 1);
			if (array == null || array.Length != 1)
			{
				throw new SyncDaemonArbitrationConfigException(Strings.ErrorCannotRetrieveSyncDaemonArbitrationConfigContainer((array == null) ? "0" : array.Length.ToString()));
			}
			return new ArbitrationConfigFromAD(array[0], ridMasterInfo);
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x000874C8 File Offset: 0x000856C8
		internal static RidMasterInfo GetRidMasterInfo(IConfigurationSession session)
		{
			bool useConfigNC = session.UseConfigNC;
			RidMasterInfo result;
			try
			{
				session.UseConfigNC = false;
				RidManagerContainer[] array = session.Find<RidManagerContainer>(null, QueryScope.SubTree, null, null, 1);
				if (array == null || array.Length != 1)
				{
					throw new RidMasterConfigException(Strings.ErrorCannotRetrieveRidManagerContainer((array == null) ? "0" : array.Length.ToString()));
				}
				session.UseConfigNC = true;
				ADObjectId fsmoRoleOwner = array[0].FsmoRoleOwner;
				if (fsmoRoleOwner == null)
				{
					throw new RidMasterConfigException(Strings.ErrorEmptyFsmoRoleOwnerAttribute);
				}
				ADServer adserver = session.Read<ADServer>(fsmoRoleOwner.Parent);
				if (adserver == null)
				{
					throw new RidMasterConfigException(Strings.ErrorCannotRetrieveServer(fsmoRoleOwner.Parent.ToString()));
				}
				string dnsHostName = adserver.DnsHostName;
				int fsmoRoleOwnerVersion = SyncDaemonArbitrationConfigHelper.GetFsmoRoleOwnerVersion(array[0].ReplicationAttributeMetadata);
				result = new RidMasterInfo(dnsHostName, fsmoRoleOwnerVersion);
			}
			finally
			{
				session.UseConfigNC = useConfigNC;
			}
			return result;
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0008759C File Offset: 0x0008579C
		internal static string GetLeaseFileName(string serviceInstanceName)
		{
			return Regex.Replace(serviceInstanceName, "[\\\\/:*?\"<>|]", "_");
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x000875B0 File Offset: 0x000857B0
		private static int GetFsmoRoleOwnerVersion(MultiValuedProperty<string> attributeMetadataList)
		{
			int num = -1;
			string text = string.Empty;
			Exception ex = null;
			if (attributeMetadataList != null)
			{
				foreach (string text2 in attributeMetadataList)
				{
					if (text2.Contains(">fSMORoleOwner</"))
					{
						text = text2;
						break;
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				int num2 = text.IndexOf("</dwVersion>");
				int num3 = text.IndexOf("<dwVersion>");
				if (num2 != -1 && num3 != -1)
				{
					string value = text.Substring(num3 + "<dwVersion>".Length, num2 - num3 - "<dwVersion>".Length);
					if (!string.IsNullOrEmpty(value))
					{
						try
						{
							num = Convert.ToInt32(value);
						}
						catch (FormatException ex2)
						{
							ex = ex2;
						}
						catch (OverflowException ex3)
						{
							ex = ex3;
						}
					}
				}
			}
			if (num == -1)
			{
				throw new SyncDaemonArbitrationConfigException(Strings.ErrorCannotParseFsmoRoleOwnerVersion(text, (ex == null) ? string.Empty : ex.ToString()));
			}
			return num;
		}

		// Token: 0x04001963 RID: 6499
		private const string FsmoRoleOwnerTag = ">fSMORoleOwner</";

		// Token: 0x04001964 RID: 6500
		private const string DwVersionStartTag = "<dwVersion>";

		// Token: 0x04001965 RID: 6501
		private const string DwVersionEndTag = "</dwVersion>";

		// Token: 0x04001966 RID: 6502
		private const string RegExInvalidFileNameCharacters = "[\\\\/:*?\"<>|]";

		// Token: 0x04001967 RID: 6503
		private const string InvalidFileNameCharacterReplacement = "_";
	}
}
