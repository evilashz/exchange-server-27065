using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200024A RID: 586
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Set", "LocalPermissions")]
	public sealed class SetLocalPermissions : Task
	{
		// Token: 0x060015CF RID: 5583 RVA: 0x0005B9B0 File Offset: 0x00059BB0
		public SetLocalPermissions()
		{
			using (Stream permissionsFileStream = SetLocalPermissions.GetPermissionsFileStream())
			{
				this.permissionsXmlDocument = new SafeXmlDocument();
				this.permissionsXmlDocument.Load(permissionsFileStream);
			}
			this.fileSystemRightsDictionary.Add("genericall", FileSystemRights.FullControl);
			this.fileSystemRightsDictionary.Add("genericread", FileSystemRights.ReadExtendedAttributes | FileSystemRights.ReadAttributes | FileSystemRights.ReadPermissions);
			this.fileSystemRightsDictionary.Add("read", FileSystemRights.ReadData);
			this.fileSystemRightsDictionary.Add("traverse", FileSystemRights.ExecuteFile);
			this.fileSystemRightsDictionary.Add("genericwrite", FileSystemRights.Write);
			this.fileSystemRightsDictionary.Add("readextendedattributes", FileSystemRights.ReadExtendedAttributes);
			this.fileSystemRightsDictionary.Add("readpermission", FileSystemRights.ReadPermissions);
			this.fileSystemRightsDictionary.Add("readattributes", FileSystemRights.ReadAttributes);
			this.fileSystemRightsDictionary.Add("deletechild", FileSystemRights.DeleteSubdirectoriesAndFiles);
			this.registryRightsDictionary.Add("genericall", RegistryRights.FullControl);
			this.registryRightsDictionary.Add("read", RegistryRights.ExecuteKey);
			this.wellKnowSecurityIdentitiesDictionary.Add("Administrators", this.GetSecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid));
			this.wellKnowSecurityIdentitiesDictionary.Add("NetworkService", this.GetSecurityIdentifier(WellKnownSidType.NetworkServiceSid));
			this.wellKnowSecurityIdentitiesDictionary.Add("LocalService", this.GetSecurityIdentifier(WellKnownSidType.LocalServiceSid));
			this.wellKnowSecurityIdentitiesDictionary.Add("AuthenticatedUser", this.GetSecurityIdentifier(WellKnownSidType.AuthenticatedUserSid));
			this.wellKnowSecurityIdentitiesDictionary.Add("System", this.GetSecurityIdentifier(WellKnownSidType.LocalSystemSid));
			this.rootRegistryKeysDictionary.Add("HKEY_CLASSES_ROOT", Registry.ClassesRoot);
			this.rootRegistryKeysDictionary.Add("HKEY_CURRENT_CONFIG", Registry.CurrentConfig);
			this.rootRegistryKeysDictionary.Add("HKEY_CURRENT_USER", Registry.CurrentUser);
			this.rootRegistryKeysDictionary.Add("HKEY_LOCAL_MACHINE", Registry.LocalMachine);
			this.rootRegistryKeysDictionary.Add("HKEY_USERS", Registry.Users);
			this.rootRegistryKeysDictionary.Add("HKEY_PERFORMANCE_DATA", Registry.PerformanceData);
			this.environmentVariablesDictionary.Add("Version", ConfigurationContext.Setup.InstalledVersion.ToString());
			this.installedPath = ConfigurationContext.Setup.InstallPath;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0005BC28 File Offset: 0x00059E28
		private SecurityIdentifier GetSecurityIdentifier(WellKnownSidType wellKnownSidType)
		{
			return new SecurityIdentifier(wellKnownSidType, null);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0005BC34 File Offset: 0x00059E34
		private static Stream GetPermissionsFileStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			return executingAssembly.GetManifestResourceStream("LocalPermissions.xml");
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0005BC52 File Offset: 0x00059E52
		// (set) Token: 0x060015D3 RID: 5587 RVA: 0x0005BC69 File Offset: 0x00059E69
		[Parameter(Mandatory = false)]
		public string Feature
		{
			get
			{
				return (string)base.Fields["Feature"];
			}
			set
			{
				base.Fields["Feature"] = value;
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0005BC7C File Offset: 0x00059E7C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			try
			{
				if (string.IsNullOrEmpty(this.Feature))
				{
					this.SetCommonPermissions();
				}
				else
				{
					this.SetFeaturePermissions(this.Feature);
				}
			}
			catch (ArgumentException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
			catch (DirectoryNotFoundException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
			}
			catch (UnauthorizedAccessException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidOperation, null);
			}
			catch (IOException exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidOperation, null);
			}
			catch (SystemException exception5)
			{
				base.WriteError(exception5, ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0005BD3C File Offset: 0x00059F3C
		private string GetTargetFolder(XmlNode targetNode)
		{
			string text = targetNode.Attributes["Path"].Value;
			text = this.ReplaceEnvironmentVariables(text);
			text = Path.Combine(this.installedPath, text ?? string.Empty);
			if (!Directory.Exists(text))
			{
				throw new ArgumentException(Strings.DirectoryDoesNotExist(text), null);
			}
			return text;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0005BD98 File Offset: 0x00059F98
		private RegistryKey GetTargetRegistryKey(XmlNode targetNode)
		{
			RegistryKey registryKey = this.rootRegistryKeysDictionary[targetNode.Attributes["Root"].Value];
			string text = targetNode.Attributes["Key"].Value;
			text = this.ReplaceEnvironmentVariables(text);
			RegistryKey registryKey2 = registryKey.OpenSubKey(text, true);
			if (registryKey2 == null)
			{
				throw new ArgumentException(Strings.RegistryKeyDoesNotExist(text, registryKey.Name), null);
			}
			return registryKey2;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0005BE0C File Offset: 0x0005A00C
		private string ReplaceEnvironmentVariables(string str)
		{
			StringBuilder stringBuilder = new StringBuilder(str);
			foreach (string text in this.environmentVariablesDictionary.Keys)
			{
				stringBuilder.Replace("$" + text, this.environmentVariablesDictionary[text]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0005BE88 File Offset: 0x0005A088
		private DirectorySecurity GetOrginalDirectorySecurity(string path)
		{
			if (!Directory.Exists(path))
			{
				throw new ArgumentException(Strings.DirectoryDoesNotExist(path), null);
			}
			return Directory.GetAccessControl(path);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0005BEAA File Offset: 0x0005A0AA
		private RegistrySecurity GetOrginalRegistrySecurity(RegistryKey key)
		{
			return key.GetAccessControl();
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0005BEB2 File Offset: 0x0005A0B2
		private void SetDirectorySecurity(string path, DirectorySecurity directorySecurity)
		{
			if (!Directory.Exists(path))
			{
				throw new ArgumentException(Strings.DirectoryDoesNotExist(path), null);
			}
			Directory.SetAccessControl(path, directorySecurity);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0005BED5 File Offset: 0x0005A0D5
		private void SetRegistrySecurity(RegistryKey key, RegistrySecurity registrySecurity)
		{
			key.SetAccessControl(registrySecurity);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0005BEDE File Offset: 0x0005A0DE
		private RegistryAccessRule CreateRegistryAccessRule(IdentityReference identity, RegistryRights rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType accessControlType)
		{
			return new RegistryAccessRule(identity, rights, inheritanceFlags, propagationFlags, accessControlType);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x0005BEEC File Offset: 0x0005A0EC
		private FileSystemAccessRule CreateFileSystemAccessRule(IdentityReference identity, FileSystemRights rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType accessControlType)
		{
			return new FileSystemAccessRule(identity, rights, inheritanceFlags, propagationFlags, accessControlType);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0005BEFA File Offset: 0x0005A0FA
		private void AddFileSystemAccessRule(DirectorySecurity permissions, FileSystemAccessRule accessRule)
		{
			permissions.AddAccessRule(accessRule);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0005BF03 File Offset: 0x0005A103
		private void AddRegistryAccessRule(RegistrySecurity permissions, RegistryAccessRule accessRule)
		{
			permissions.AddAccessRule(accessRule);
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0005BF0C File Offset: 0x0005A10C
		private void RemoveFileSystemAccessRuleAll(DirectorySecurity permissions, SecurityIdentifier securityIdentifier)
		{
			permissions.RemoveAccessRuleAll(new FileSystemAccessRule(securityIdentifier, FileSystemRights.FullControl, AccessControlType.Allow));
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0005BF20 File Offset: 0x0005A120
		private void RemoveRegistryAccessRuleAll(RegistrySecurity permissions, SecurityIdentifier securityIdentifier)
		{
			permissions.RemoveAccessRuleAll(new RegistryAccessRule(securityIdentifier, RegistryRights.FullControl, AccessControlType.Allow));
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0005BF34 File Offset: 0x0005A134
		private void SetCommonPermissions()
		{
			XmlNode permissionsOnCurrentLevel = this.permissionsXmlDocument.SelectSingleNode(string.Format("{0}/{1}", "Permissions", "CommonPermissionSet"));
			this.SetPermissionsOnCurrentLevel(permissionsOnCurrentLevel);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0005BF68 File Offset: 0x0005A168
		private void SetFeaturePermissions(string feature)
		{
			TaskLogger.LogEnter();
			XmlNode xmlNode = this.permissionsXmlDocument.SelectSingleNode(string.Format("{0}/{1}[@{2}='{3}']", new object[]
			{
				"Permissions",
				"FeaturePermissionSet",
				"Name",
				feature
			}));
			if (xmlNode != null)
			{
				this.SetPermissionsOnCurrentLevel(xmlNode);
				using (XmlNodeList xmlNodeList = xmlNode.SelectNodes("SharedPermissionSet"))
				{
					foreach (object obj in xmlNodeList)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						string value = xmlNode2.Attributes["Name"].Value;
						XmlNode permissionsOnCurrentLevel = this.permissionsXmlDocument.SelectSingleNode(string.Format("{0}/{1}[@{2}='{3}']", new object[]
						{
							"Permissions",
							"SharedPermissionSet",
							"Name",
							value
						}));
						this.SetPermissionsOnCurrentLevel(permissionsOnCurrentLevel);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0005C098 File Offset: 0x0005A298
		private void SetPermissionsOnCurrentLevel(XmlNode permissionSetNode)
		{
			this.SetPermissionsOnCurrentLevel<string, DirectorySecurity, FileSystemAccessRule, FileSystemRights>(permissionSetNode, "Folder", this.fileSystemRightsDictionary, new SetLocalPermissions.GetTarget<string>(this.GetTargetFolder), new SetLocalPermissions.GetOrginalPermissionsOnTarget<DirectorySecurity, string>(this.GetOrginalDirectorySecurity), new SetLocalPermissions.SetPermissionsOnTarget<DirectorySecurity, string>(this.SetDirectorySecurity), new SetLocalPermissions.CreateAccessRule<FileSystemAccessRule, FileSystemRights>(this.CreateFileSystemAccessRule), new SetLocalPermissions.AddAccessRule<DirectorySecurity, FileSystemAccessRule>(this.AddFileSystemAccessRule), new SetLocalPermissions.RemoveAccessRuleAll<DirectorySecurity>(this.RemoveFileSystemAccessRuleAll));
			this.SetPermissionsOnCurrentLevel<RegistryKey, RegistrySecurity, RegistryAccessRule, RegistryRights>(permissionSetNode, "Registry", this.registryRightsDictionary, new SetLocalPermissions.GetTarget<RegistryKey>(this.GetTargetRegistryKey), new SetLocalPermissions.GetOrginalPermissionsOnTarget<RegistrySecurity, RegistryKey>(this.GetOrginalRegistrySecurity), new SetLocalPermissions.SetPermissionsOnTarget<RegistrySecurity, RegistryKey>(this.SetRegistrySecurity), new SetLocalPermissions.CreateAccessRule<RegistryAccessRule, RegistryRights>(this.CreateRegistryAccessRule), new SetLocalPermissions.AddAccessRule<RegistrySecurity, RegistryAccessRule>(this.AddRegistryAccessRule), new SetLocalPermissions.RemoveAccessRuleAll<RegistrySecurity>(this.RemoveRegistryAccessRuleAll));
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x0005C15C File Offset: 0x0005A35C
		private void SetPermissionsOnCurrentLevel<TTarget, TSecurity, TAccessRule, TRights>(XmlNode permissionSetNode, string targetType, Dictionary<string, TRights> rightsDictionary, SetLocalPermissions.GetTarget<TTarget> getTarget, SetLocalPermissions.GetOrginalPermissionsOnTarget<TSecurity, TTarget> getOrginalPermissionsOnTarget, SetLocalPermissions.SetPermissionsOnTarget<TSecurity, TTarget> setPermissionsOnTarget, SetLocalPermissions.CreateAccessRule<TAccessRule, TRights> createAccessRule, SetLocalPermissions.AddAccessRule<TSecurity, TAccessRule> addAccessRule, SetLocalPermissions.RemoveAccessRuleAll<TSecurity> removeAccessRuleAll) where TTarget : class where TSecurity : NativeObjectSecurity, new() where TAccessRule : AccessRule
		{
			TaskLogger.LogEnter();
			using (XmlNodeList xmlNodeList = permissionSetNode.SelectNodes(targetType))
			{
				foreach (object obj in xmlNodeList)
				{
					XmlNode targetNode = (XmlNode)obj;
					this.ChangePermissions<TTarget, TSecurity, TAccessRule, TRights>(targetNode, rightsDictionary, getTarget, getOrginalPermissionsOnTarget, setPermissionsOnTarget, createAccessRule, addAccessRule, removeAccessRuleAll);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x0005C1E8 File Offset: 0x0005A3E8
		private void ChangePermissions<TTarget, TSecurity, TAccessRule, TRights>(XmlNode targetNode, Dictionary<string, TRights> rightsDictionary, SetLocalPermissions.GetTarget<TTarget> getTarget, SetLocalPermissions.GetOrginalPermissionsOnTarget<TSecurity, TTarget> getOrginalPermissionsOnTarget, SetLocalPermissions.SetPermissionsOnTarget<TSecurity, TTarget> setPermissionsOnTarget, SetLocalPermissions.CreateAccessRule<TAccessRule, TRights> createAccessRule, SetLocalPermissions.AddAccessRule<TSecurity, TAccessRule> addAccessRule, SetLocalPermissions.RemoveAccessRuleAll<TSecurity> removeAccessRuleAll) where TTarget : class where TSecurity : NativeObjectSecurity, new() where TAccessRule : AccessRule
		{
			TaskLogger.LogEnter();
			TTarget target = getTarget(targetNode);
			TSecurity tsecurity = default(TSecurity);
			if (targetNode.Attributes["Sddl"] != null)
			{
				string value = targetNode.Attributes["Sddl"].Value;
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException(null, "Sddl");
				}
				if (targetNode.Attributes.Count > 2)
				{
					foreach (object obj in targetNode.Attributes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						if (xmlNode.Name != "Sddl" && xmlNode.Name != "Path")
						{
							throw new ArgumentException(null, xmlNode.Name);
						}
					}
				}
				if (targetNode.ChildNodes.Count > 0)
				{
					throw new ArgumentException(null, targetNode.ChildNodes[0].Name);
				}
				tsecurity = Activator.CreateInstance<TSecurity>();
				tsecurity.SetSecurityDescriptorSddlForm(value);
			}
			else
			{
				tsecurity = getOrginalPermissionsOnTarget(target);
				if (tsecurity.AreAccessRulesCanonical)
				{
					tsecurity.SetAccessRuleProtection(this.IsProtected(targetNode), this.PreserveInheritance(targetNode));
				}
				else
				{
					tsecurity = Activator.CreateInstance<TSecurity>();
				}
				using (XmlNodeList xmlNodeList = targetNode.SelectNodes("Permission"))
				{
					foreach (object obj2 in xmlNodeList)
					{
						XmlNode permissionNode = (XmlNode)obj2;
						SecurityIdentifier securityIdentifier = this.GetSecurityIdentifier(permissionNode);
						InheritanceFlags inheritanceFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
						PropagationFlags propagationFlags = PropagationFlags.None;
						if (this.IsProtected(targetNode) && this.PreserveInheritance(targetNode) && !this.IsExtended(permissionNode))
						{
							removeAccessRuleAll(tsecurity, securityIdentifier);
						}
						List<SetLocalPermissions.RightsWithAccessControlType<TRights>> rights = this.GetRights<TRights>(permissionNode, rightsDictionary);
						foreach (SetLocalPermissions.RightsWithAccessControlType<TRights> rightsWithAccessControlType in rights)
						{
							addAccessRule(tsecurity, createAccessRule(securityIdentifier, rightsWithAccessControlType.Rights, inheritanceFlags, propagationFlags, rightsWithAccessControlType.AccessControlType));
						}
					}
				}
			}
			setPermissionsOnTarget(target, tsecurity);
			TaskLogger.LogExit();
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x0005C46C File Offset: 0x0005A66C
		private bool IsProtected(XmlNode targetNode)
		{
			return targetNode.Attributes["Protected"] != null && string.Compare(targetNode.Attributes["Protected"].Value, "yes", true) == 0;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0005C4A5 File Offset: 0x0005A6A5
		private bool PreserveInheritance(XmlNode targetNode)
		{
			return targetNode.Attributes["PreserveInheritance"] == null || string.Compare(targetNode.Attributes["PreserveInheritance"].Value, "yes", true) == 0;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0005C4E0 File Offset: 0x0005A6E0
		private SecurityIdentifier GetSecurityIdentifier(XmlNode permissionNode)
		{
			string value = permissionNode.Attributes["User"].Value;
			return this.wellKnowSecurityIdentitiesDictionary[value];
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x0005C511 File Offset: 0x0005A711
		private bool IsExtended(XmlNode permissionNode)
		{
			return permissionNode.Attributes["Extended"] != null && string.Compare(permissionNode.Attributes["Extended"].Value, "yes", true) == 0;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0005C54C File Offset: 0x0005A74C
		private List<SetLocalPermissions.RightsWithAccessControlType<TRights>> GetRights<TRights>(XmlNode permissionNode, Dictionary<string, TRights> rightsDictionary)
		{
			TaskLogger.LogEnter();
			List<SetLocalPermissions.RightsWithAccessControlType<TRights>> list = new List<SetLocalPermissions.RightsWithAccessControlType<TRights>>();
			foreach (object obj in permissionNode.Attributes)
			{
				XmlAttribute permissionAttribute = (XmlAttribute)obj;
				SetLocalPermissions.RightsWithAccessControlType<TRights> rights = this.GetRights<TRights>(permissionAttribute, rightsDictionary);
				if (rights != null)
				{
					list.Add(rights);
				}
			}
			TaskLogger.LogExit();
			return list;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0005C5C8 File Offset: 0x0005A7C8
		private SetLocalPermissions.RightsWithAccessControlType<TRights> GetRights<TRights>(XmlAttribute permissionAttribute, Dictionary<string, TRights> rightsDictionary)
		{
			TaskLogger.LogEnter();
			SetLocalPermissions.RightsWithAccessControlType<TRights> rightsWithAccessControlType = null;
			if (permissionAttribute.Name != "User" && permissionAttribute.Name != "Extended")
			{
				rightsWithAccessControlType = new SetLocalPermissions.RightsWithAccessControlType<TRights>();
				rightsWithAccessControlType.Rights = rightsDictionary[permissionAttribute.Name.ToLower()];
				if (string.Compare(permissionAttribute.Value, "yes", true) == 0)
				{
					rightsWithAccessControlType.AccessControlType = AccessControlType.Allow;
				}
				else
				{
					rightsWithAccessControlType.AccessControlType = AccessControlType.Deny;
				}
			}
			TaskLogger.LogExit();
			return rightsWithAccessControlType;
		}

		// Token: 0x0400097D RID: 2429
		private const string PermissionsFileName = "LocalPermissions.xml";

		// Token: 0x0400097E RID: 2430
		private const string RootNode = "Permissions";

		// Token: 0x0400097F RID: 2431
		private const string FeaturePermissionSetNode = "FeaturePermissionSet";

		// Token: 0x04000980 RID: 2432
		private const string CommonPermissionSetNode = "CommonPermissionSet";

		// Token: 0x04000981 RID: 2433
		private const string SharedPermissionSetNode = "SharedPermissionSet";

		// Token: 0x04000982 RID: 2434
		private const string PermissionSetNodeNameAttribute = "Name";

		// Token: 0x04000983 RID: 2435
		private const string FolderNode = "Folder";

		// Token: 0x04000984 RID: 2436
		private const string FolderNodePathAttribute = "Path";

		// Token: 0x04000985 RID: 2437
		private const string PermissionNode = "Permission";

		// Token: 0x04000986 RID: 2438
		private const string PermissionNodeUserAttribute = "User";

		// Token: 0x04000987 RID: 2439
		private const string PermissionNodeExtendedAttribute = "Extended";

		// Token: 0x04000988 RID: 2440
		private const string RegistryNode = "Registry";

		// Token: 0x04000989 RID: 2441
		private const string RegistryNodeRootAttribute = "Root";

		// Token: 0x0400098A RID: 2442
		private const string RegistryNodeKeyAttribute = "Key";

		// Token: 0x0400098B RID: 2443
		private const string TargetNodeProtectedAttribute = "Protected";

		// Token: 0x0400098C RID: 2444
		private const string TargetNodePreserveInheritanceAttribute = "PreserveInheritance";

		// Token: 0x0400098D RID: 2445
		private const string SddlAttribute = "Sddl";

		// Token: 0x0400098E RID: 2446
		private const string ClassRootBaseKey = "HKEY_CLASSES_ROOT";

		// Token: 0x0400098F RID: 2447
		private const string CurrentConfigBaseKey = "HKEY_CURRENT_CONFIG";

		// Token: 0x04000990 RID: 2448
		private const string CurrentUserBaseKey = "HKEY_CURRENT_USER";

		// Token: 0x04000991 RID: 2449
		private const string LocalMachineBaseKey = "HKEY_LOCAL_MACHINE";

		// Token: 0x04000992 RID: 2450
		private const string UsersBaseKey = "HKEY_USERS";

		// Token: 0x04000993 RID: 2451
		private const string DynDataBaseKey = "HKEY_DYN_DATA";

		// Token: 0x04000994 RID: 2452
		private const string PerformanceDataBaseKey = "HKEY_PERFORMANCE_DATA";

		// Token: 0x04000995 RID: 2453
		private const string AdministratorsName = "Administrators";

		// Token: 0x04000996 RID: 2454
		private const string NetworkServiceName = "NetworkService";

		// Token: 0x04000997 RID: 2455
		private const string LocalServiceName = "LocalService";

		// Token: 0x04000998 RID: 2456
		private const string AuthenticatedUserName = "AuthenticatedUser";

		// Token: 0x04000999 RID: 2457
		private const string SystemName = "System";

		// Token: 0x0400099A RID: 2458
		private const string PrefixOfEnvironmentVariable = "$";

		// Token: 0x0400099B RID: 2459
		private const string Version = "Version";

		// Token: 0x0400099C RID: 2460
		private SafeXmlDocument permissionsXmlDocument;

		// Token: 0x0400099D RID: 2461
		private Dictionary<string, FileSystemRights> fileSystemRightsDictionary = new Dictionary<string, FileSystemRights>();

		// Token: 0x0400099E RID: 2462
		private Dictionary<string, RegistryRights> registryRightsDictionary = new Dictionary<string, RegistryRights>();

		// Token: 0x0400099F RID: 2463
		private Dictionary<string, SecurityIdentifier> wellKnowSecurityIdentitiesDictionary = new Dictionary<string, SecurityIdentifier>();

		// Token: 0x040009A0 RID: 2464
		private Dictionary<string, RegistryKey> rootRegistryKeysDictionary = new Dictionary<string, RegistryKey>();

		// Token: 0x040009A1 RID: 2465
		private Dictionary<string, string> environmentVariablesDictionary = new Dictionary<string, string>();

		// Token: 0x040009A2 RID: 2466
		private readonly string installedPath;

		// Token: 0x0200024B RID: 587
		// (Invoke) Token: 0x060015EE RID: 5614
		private delegate TTarget GetTarget<TTarget>(XmlNode targetNode) where TTarget : class;

		// Token: 0x0200024C RID: 588
		// (Invoke) Token: 0x060015F2 RID: 5618
		private delegate TSecurity GetOrginalPermissionsOnTarget<TSecurity, TTarget>(TTarget target) where TSecurity : NativeObjectSecurity where TTarget : class;

		// Token: 0x0200024D RID: 589
		// (Invoke) Token: 0x060015F6 RID: 5622
		private delegate void SetPermissionsOnTarget<TSecurity, TTarget>(TTarget target, TSecurity security) where TSecurity : NativeObjectSecurity where TTarget : class;

		// Token: 0x0200024E RID: 590
		// (Invoke) Token: 0x060015FA RID: 5626
		private delegate TAccessRule CreateAccessRule<TAccessRule, TRights>(IdentityReference identity, TRights rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType accessControlType) where TAccessRule : AccessRule;

		// Token: 0x0200024F RID: 591
		// (Invoke) Token: 0x060015FE RID: 5630
		private delegate void AddAccessRule<TSecurity, TAccessRule>(TSecurity permissions, TAccessRule accessRule) where TSecurity : NativeObjectSecurity where TAccessRule : AccessRule;

		// Token: 0x02000250 RID: 592
		// (Invoke) Token: 0x06001602 RID: 5634
		private delegate void RemoveAccessRuleAll<TSecurity>(TSecurity permissions, SecurityIdentifier securityIdentifier) where TSecurity : NativeObjectSecurity;

		// Token: 0x02000251 RID: 593
		private class RightsWithAccessControlType<TRights>
		{
			// Token: 0x040009A3 RID: 2467
			public TRights Rights;

			// Token: 0x040009A4 RID: 2468
			public AccessControlType AccessControlType;
		}
	}
}
