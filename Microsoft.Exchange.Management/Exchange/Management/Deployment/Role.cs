using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000174 RID: 372
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class Role : IInstallable
	{
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0003FC7C File Offset: 0x0003DE7C
		public virtual bool IsUnpacked
		{
			get
			{
				Version unpackedVersion = RolesUtility.GetUnpackedVersion(this.RoleName);
				return !(unpackedVersion == null);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0003FCA4 File Offset: 0x0003DEA4
		public virtual Version UnpackedVersion
		{
			get
			{
				Version unpackedVersion = RolesUtility.GetUnpackedVersion(this.RoleName);
				if (unpackedVersion == null)
				{
					return new Version(0, 0, 0, 0);
				}
				return unpackedVersion;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0003FCD4 File Offset: 0x0003DED4
		public virtual bool IsDatacenterUnpacked
		{
			get
			{
				Version unpackedDatacenterVersion = RolesUtility.GetUnpackedDatacenterVersion(this.RoleName);
				return !(unpackedDatacenterVersion == null);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0003FCFC File Offset: 0x0003DEFC
		public virtual Version UnpackedDatacenterVersion
		{
			get
			{
				Version unpackedDatacenterVersion = RolesUtility.GetUnpackedDatacenterVersion(this.RoleName);
				if (unpackedDatacenterVersion == null)
				{
					return new Version(0, 0, 0, 0);
				}
				return unpackedDatacenterVersion;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0003FD2C File Offset: 0x0003DF2C
		public virtual bool IsInstalled
		{
			get
			{
				Version v = Role.GetRoleConfiguredVersion(this.RoleName);
				return !(v == null);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0003FD58 File Offset: 0x0003DF58
		public virtual Version InstalledVersion
		{
			get
			{
				Version configuredVersion = RolesUtility.GetConfiguredVersion(this.RoleName);
				if (configuredVersion == null)
				{
					return new Version(0, 0, 0, 0);
				}
				return configuredVersion;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0003FD88 File Offset: 0x0003DF88
		public virtual bool IsPartiallyInstalled
		{
			get
			{
				ConfigurationStatus configurationStatus = new ConfigurationStatus(this.RoleName);
				RolesUtility.GetConfiguringStatus(ref configurationStatus);
				return (configurationStatus.Action != InstallationModes.Unknown && configurationStatus.Watermark != null) || this.IsMissingPostSetup;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0003FDC8 File Offset: 0x0003DFC8
		public virtual bool IsMissingPostSetup
		{
			get
			{
				Version unpackedVersion = RolesUtility.GetUnpackedVersion(this.RoleName);
				Version configuredVersion = RolesUtility.GetConfiguredVersion(this.RoleName);
				if (configuredVersion == null || configuredVersion != unpackedVersion)
				{
					return false;
				}
				Version postSetupVersion = RolesUtility.GetPostSetupVersion(this.RoleName);
				return postSetupVersion != null && postSetupVersion != configuredVersion;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0003FE1F File Offset: 0x0003E01F
		public virtual bool IsCurrent
		{
			get
			{
				return this.InstalledVersion == ConfigurationContext.Setup.InstalledVersion;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0003FE31 File Offset: 0x0003E031
		public virtual bool IsDatacenterOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0003FE34 File Offset: 0x0003E034
		public virtual bool IsDatacenterDedicatedOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0003FE37 File Offset: 0x0003E037
		public virtual string InstalledPath
		{
			get
			{
				if (this.IsInstalled)
				{
					return ConfigurationContext.Setup.InstallPath;
				}
				return "";
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000DE4 RID: 3556
		public abstract ServerRole ServerRole { get; }

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000DE5 RID: 3557
		public abstract Task InstallTask { get; }

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000DE6 RID: 3558
		public abstract Task DisasterRecoveryTask { get; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000DE7 RID: 3559
		public abstract Task UninstallTask { get; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000DE8 RID: 3560
		public abstract ValidatingTask ValidateTask { get; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0003FE4C File Offset: 0x0003E04C
		public virtual string RoleName
		{
			get
			{
				return this.roleName;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0003FE54 File Offset: 0x0003E054
		internal virtual SetupComponentInfoCollection AllComponents
		{
			get
			{
				if (this.allComponents == null)
				{
					this.LoadComponentList();
				}
				return this.allComponents;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0003FE6A File Offset: 0x0003E06A
		internal static string SetupComponentInfoFilePath
		{
			get
			{
				if (Path.GetDirectoryName(ConfigurationContext.Setup.AssemblyPath).ToLowerInvariant() == Path.GetDirectoryName(ConfigurationContext.Setup.BinPath).ToLowerInvariant())
				{
					return ConfigurationContext.Setup.InstallPath;
				}
				return ConfigurationContext.Setup.AssemblyPath;
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0003FE9C File Offset: 0x0003E09C
		protected void LoadComponentList()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.RoleName
			});
			string fileName = Path.Combine(ConfigurationContext.Setup.AssemblyPath, this.RoleName + "Definition.xml");
			this.allComponents = new SetupComponentInfoCollection();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RoleDefinition.xsd");
			xmlReaderSettings.Schemas.Add(null, XmlReader.Create(manifestResourceStream));
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(SetupComponentInfoReferenceCollection));
			string text;
			using (XmlReader xmlReader = RolesUtility.CreateXmlReader(fileName, xmlReaderSettings, out text))
			{
				TaskLogger.Log(Strings.ReadingComponents(this.RoleName, text));
				SetupComponentInfoReferenceCollection setupComponentInfoReferenceCollection = null;
				try
				{
					setupComponentInfoReferenceCollection = (SetupComponentInfoReferenceCollection)xmlSerializer.Deserialize(xmlReader);
				}
				catch (InvalidOperationException ex)
				{
					throw new XmlDeserializationException(text, ex.Message, (ex.InnerException == null) ? string.Empty : ex.InnerException.Message);
				}
				TaskLogger.Log(Strings.FoundComponents(setupComponentInfoReferenceCollection.Count));
				foreach (SetupComponentInfoReference reference in setupComponentInfoReferenceCollection)
				{
					SetupComponentInfo item = this.LoadComponent(reference);
					this.allComponents.Add(item);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00040020 File Offset: 0x0003E220
		protected SetupComponentInfo LoadComponent(SetupComponentInfoReference reference)
		{
			TaskLogger.LogEnter(new object[]
			{
				this.RoleName,
				reference.RelativeFileLocation
			});
			string fileName = Path.Combine(Role.SetupComponentInfoFilePath, reference.RelativeFileLocation);
			SetupComponentInfo result = RolesUtility.ReadSetupComponentInfoFile(fileName);
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0004006C File Offset: 0x0003E26C
		internal void Reset()
		{
			this.allComponents = null;
		}

		// Token: 0x040006A1 RID: 1697
		protected string roleName;

		// Token: 0x040006A2 RID: 1698
		internal SetupComponentInfoCollection allComponents;

		// Token: 0x040006A3 RID: 1699
		internal static Func<string, Version> GetRoleConfiguredVersion = (string currentRoleName) => RolesUtility.GetConfiguredVersion(currentRoleName);
	}
}
