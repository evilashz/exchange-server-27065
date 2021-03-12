using System;
using System.Globalization;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000122 RID: 290
	internal static class ObjectSecurity
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0003149C File Offset: 0x0002F69C
		public static DirectorySecurity ExchangeFolderSecurity
		{
			get
			{
				string text = ObjectSecurity.ExchangeServersUsgSid.ToString();
				string sddlForm = string.Format(CultureInfo.InvariantCulture, "O:BAG:SYD:P(A;OICI;FA;;;SY)(A;OICI;FA;;;BA)(A;OICI;GR;;;{0})", new object[]
				{
					text
				});
				DirectorySecurity directorySecurity = new DirectorySecurity();
				directorySecurity.SetSecurityDescriptorSddlForm(sddlForm, AccessControlSections.Access);
				return directorySecurity;
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000314E0 File Offset: 0x0002F6E0
		public static void AddRulesToSharedDirectory(string directory)
		{
			DirectorySecurity accessControl = Directory.GetAccessControl(directory);
			foreach (FileSystemAccessRule rule in ObjectSecurity.SharedDirectoryRules)
			{
				accessControl.AddAccessRule(rule);
			}
			Directory.SetAccessControl(directory, accessControl);
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0003151C File Offset: 0x0002F71C
		public static FileSystemSecurity NetShareSecurity
		{
			get
			{
				string text = ObjectSecurity.ExchangeServersUsgSid.ToString();
				string sddlForm = string.Format(CultureInfo.InvariantCulture, "D:P(A;;{1};;;{0})", new object[]
				{
					text,
					"0x120089"
				});
				FileSystemSecurity fileSystemSecurity = new DirectorySecurity();
				fileSystemSecurity.SetSecurityDescriptorSddlForm(sddlForm, AccessControlSections.Access);
				return fileSystemSecurity;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00031567 File Offset: 0x0002F767
		public static byte[] NetShareSecurityBinaryForm
		{
			get
			{
				return ObjectSecurity.NetShareSecurity.GetSecurityDescriptorBinaryForm();
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00031574 File Offset: 0x0002F774
		public static FileSecurity TemporaryFileSecurity
		{
			get
			{
				string text = null;
				using (WindowsIdentity current = WindowsIdentity.GetCurrent())
				{
					text = current.User.Value;
				}
				string sddlForm = string.Format(CultureInfo.InvariantCulture, "D:P(A;;FRFWSD;;;{0})(A;;SD;;;BA)", new object[]
				{
					text
				});
				FileSecurity fileSecurity = new FileSecurity();
				fileSecurity.SetSecurityDescriptorSddlForm(sddlForm, AccessControlSections.Access);
				return fileSecurity;
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000315E0 File Offset: 0x0002F7E0
		private static FileSecurity CreateBaseRpcSecurityObject()
		{
			FileSecurity fileSecurity = new FileSecurity();
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
			FileSystemAccessRule fileSystemAccessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.SetOwner(securityIdentifier);
			fileSecurity.SetAccessRule(fileSystemAccessRule);
			SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
			fileSystemAccessRule = new FileSystemAccessRule(identity, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.AddAccessRule(fileSystemAccessRule);
			SecurityIdentifier identity2 = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
			fileSystemAccessRule = new FileSystemAccessRule(identity2, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.AddAccessRule(fileSystemAccessRule);
			fileSystemAccessRule = new FileSystemAccessRule(ObjectSecurity.ExchangeServersUsgSid, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.AddAccessRule(fileSystemAccessRule);
			return fileSecurity;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0003165C File Offset: 0x0002F85C
		public static FileSecurity BaseRpcSecurity
		{
			get
			{
				return ObjectSecurity.CreateBaseRpcSecurityObject();
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00031663 File Offset: 0x0002F863
		public static FileSecurity ActiveManagerRpcSecurity
		{
			get
			{
				return ObjectSecurity.BaseRpcSecurity;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0003166A File Offset: 0x0002F86A
		public static FileSecurity ReplayRpcSecurity
		{
			get
			{
				return ObjectSecurity.BaseRpcSecurity;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00031674 File Offset: 0x0002F874
		private static FileSystemAccessRule[] SharedDirectoryRules
		{
			get
			{
				return new FileSystemAccessRule[]
				{
					new FileSystemAccessRule(ObjectSecurity.ExchangeServersUsgSid, FileSystemRights.Read, InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow)
				};
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x000316A0 File Offset: 0x0002F8A0
		public static SecurityIdentifier ExchangeServersUsgSid
		{
			get
			{
				IADRootOrganizationRecipientSession iadrootOrganizationRecipientSession = ADSessionFactory.CreateIgnoreInvalidRootOrgRecipientSession();
				return iadrootOrganizationRecipientSession.GetExchangeServersUsgSid();
			}
		}

		// Token: 0x040004A3 RID: 1187
		public const int RetrySleepDurationMilliSecs = 100;

		// Token: 0x040004A4 RID: 1188
		private static readonly TimeSpan ADRetryDelay = new TimeSpan(0, 0, 3);
	}
}
