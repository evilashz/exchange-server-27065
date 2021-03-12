using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02001216 RID: 4630
	internal static class Strings
	{
		// Token: 0x0600BA8A RID: 47754 RVA: 0x002A850C File Offset: 0x002A670C
		static Strings()
		{
			Strings.stringIDs.Add(774587054U, "GatewayRoleIsNotUnpacked");
			Strings.stringIDs.Add(2478794571U, "AntispamUpdateServiceDescription");
			Strings.stringIDs.Add(988899670U, "AdamServiceFailedToUninstall");
			Strings.stringIDs.Add(2393646509U, "TransportLogSearchServiceDisplayName");
			Strings.stringIDs.Add(175509160U, "UninstallEdgeTransportServiceTask");
			Strings.stringIDs.Add(4124898948U, "UninstallEdgeSyncServiceTask");
			Strings.stringIDs.Add(2736467170U, "AntispamUpdateServiceDisplayName");
			Strings.stringIDs.Add(1953906065U, "InstallAuditTask");
			Strings.stringIDs.Add(2960500387U, "EdgeTransportServiceNotUninstalled");
			Strings.stringIDs.Add(4049824753U, "InstallEdgeTransportServiceTask");
			Strings.stringIDs.Add(2949158791U, "EdgeTransportServiceDescription");
			Strings.stringIDs.Add(2685419127U, "ServiceAlreadyInstalled");
			Strings.stringIDs.Add(576724013U, "SslPortSameAsLdapPort");
			Strings.stringIDs.Add(159382586U, "EdgeTransportServiceNotInstalled");
			Strings.stringIDs.Add(4271619264U, "TransportLogSearchServiceDescription");
			Strings.stringIDs.Add(1444113173U, "InstallEdgeSyncServiceTask");
			Strings.stringIDs.Add(3376781594U, "TransportLogSearchServiceNotUninstalled");
			Strings.stringIDs.Add(2900475730U, "EdgeCredentialServiceDisplayName");
			Strings.stringIDs.Add(767347686U, "UninstallAdamTask");
			Strings.stringIDs.Add(3473798678U, "UninstallAntispamUpdateServiceTask");
			Strings.stringIDs.Add(399958817U, "AdamServiceFailedToInstall");
			Strings.stringIDs.Add(4113916114U, "AntimalwareServiceDescription");
			Strings.stringIDs.Add(3105007247U, "EdgeSyncServiceDescription");
			Strings.stringIDs.Add(683215962U, "InstallAntimalwareServiceTask");
			Strings.stringIDs.Add(3584000439U, "PackagePath");
			Strings.stringIDs.Add(2829653254U, "FmsServiceNotInstalled");
			Strings.stringIDs.Add(871484781U, "InstallFmsServiceTask");
			Strings.stringIDs.Add(925743836U, "FmsServiceDisplayName");
			Strings.stringIDs.Add(2657970248U, "UninstallFmsServiceTask");
			Strings.stringIDs.Add(2391764367U, "EdgeCredentialServiceDescription");
			Strings.stringIDs.Add(3216105370U, "UninstallAuditTask");
			Strings.stringIDs.Add(292208835U, "FmsServiceNotUninstalled");
			Strings.stringIDs.Add(2034142329U, "InstallAntispamUpdateServiceTask");
			Strings.stringIDs.Add(1498913134U, "EdgeSyncServiceNotInstalled");
			Strings.stringIDs.Add(140982033U, "InvalidLdapFileName");
			Strings.stringIDs.Add(1307180415U, "AntimalwareServiceNotInstalled");
			Strings.stringIDs.Add(4193735315U, "AntimalwareServiceDisplayName");
			Strings.stringIDs.Add(1415188871U, "FmsServiceDescription");
			Strings.stringIDs.Add(3248437725U, "AdamServiceNotInstalled");
			Strings.stringIDs.Add(2855011956U, "EdgeSyncServiceDisplayName");
			Strings.stringIDs.Add(3104026373U, "UninstallAntimalwareServiceTask");
			Strings.stringIDs.Add(2915042048U, "InstallAdamSchemaTask");
			Strings.stringIDs.Add(3430669213U, "Port");
			Strings.stringIDs.Add(1957953568U, "EdgeCredentialServiceNotInstalled");
			Strings.stringIDs.Add(3698153928U, "EdgeTransportServiceDisplayName");
			Strings.stringIDs.Add(1795457964U, "AntimalwareServiceNotUninstalled");
			Strings.stringIDs.Add(1454104731U, "EdgeCredentialServiceNotUninstalled");
			Strings.stringIDs.Add(2145801173U, "InstallAdamTask");
			Strings.stringIDs.Add(4104091810U, "AdamServiceDescription");
			Strings.stringIDs.Add(303188103U, "AdamInstallFailureDataOrLogFolderNotEmpty");
			Strings.stringIDs.Add(2126302003U, "NoPathArgument");
			Strings.stringIDs.Add(2777897381U, "TransportLogSearchServiceNotInstalled");
			Strings.stringIDs.Add(1507908660U, "InstallDir");
			Strings.stringIDs.Add(2869926485U, "AdamServiceDisplayName");
			Strings.stringIDs.Add(523929713U, "InvalidPackagePathValue");
			Strings.stringIDs.Add(830945707U, "EdgeSyncServiceNotUninstalled");
			Strings.stringIDs.Add(588066505U, "UninstallOldEdgeTransportServiceTask");
		}

		// Token: 0x17003A9C RID: 15004
		// (get) Token: 0x0600BA8B RID: 47755 RVA: 0x002A89BC File Offset: 0x002A6BBC
		public static LocalizedString GatewayRoleIsNotUnpacked
		{
			get
			{
				return new LocalizedString("GatewayRoleIsNotUnpacked", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A9D RID: 15005
		// (get) Token: 0x0600BA8C RID: 47756 RVA: 0x002A89DA File Offset: 0x002A6BDA
		public static LocalizedString AntispamUpdateServiceDescription
		{
			get
			{
				return new LocalizedString("AntispamUpdateServiceDescription", "ExAC0AF2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A9E RID: 15006
		// (get) Token: 0x0600BA8D RID: 47757 RVA: 0x002A89F8 File Offset: 0x002A6BF8
		public static LocalizedString AdamServiceFailedToUninstall
		{
			get
			{
				return new LocalizedString("AdamServiceFailedToUninstall", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A9F RID: 15007
		// (get) Token: 0x0600BA8E RID: 47758 RVA: 0x002A8A16 File Offset: 0x002A6C16
		public static LocalizedString TransportLogSearchServiceDisplayName
		{
			get
			{
				return new LocalizedString("TransportLogSearchServiceDisplayName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AA0 RID: 15008
		// (get) Token: 0x0600BA8F RID: 47759 RVA: 0x002A8A34 File Offset: 0x002A6C34
		public static LocalizedString UninstallEdgeTransportServiceTask
		{
			get
			{
				return new LocalizedString("UninstallEdgeTransportServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AA1 RID: 15009
		// (get) Token: 0x0600BA90 RID: 47760 RVA: 0x002A8A52 File Offset: 0x002A6C52
		public static LocalizedString UninstallEdgeSyncServiceTask
		{
			get
			{
				return new LocalizedString("UninstallEdgeSyncServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AA2 RID: 15010
		// (get) Token: 0x0600BA91 RID: 47761 RVA: 0x002A8A70 File Offset: 0x002A6C70
		public static LocalizedString AntispamUpdateServiceDisplayName
		{
			get
			{
				return new LocalizedString("AntispamUpdateServiceDisplayName", "ExACD0AC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA92 RID: 47762 RVA: 0x002A8A90 File Offset: 0x002A6C90
		public static LocalizedString PathTooLong(string path)
		{
			return new LocalizedString("PathTooLong", "", false, false, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x0600BA93 RID: 47763 RVA: 0x002A8AC0 File Offset: 0x002A6CC0
		public static LocalizedString AdamUninstallError(string error)
		{
			return new LocalizedString("AdamUninstallError", "", false, false, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17003AA3 RID: 15011
		// (get) Token: 0x0600BA94 RID: 47764 RVA: 0x002A8AEF File Offset: 0x002A6CEF
		public static LocalizedString InstallAuditTask
		{
			get
			{
				return new LocalizedString("InstallAuditTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AA4 RID: 15012
		// (get) Token: 0x0600BA95 RID: 47765 RVA: 0x002A8B0D File Offset: 0x002A6D0D
		public static LocalizedString EdgeTransportServiceNotUninstalled
		{
			get
			{
				return new LocalizedString("EdgeTransportServiceNotUninstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA96 RID: 47766 RVA: 0x002A8B2C File Offset: 0x002A6D2C
		public static LocalizedString InvalidAdamInstanceName(string instanceName)
		{
			return new LocalizedString("InvalidAdamInstanceName", "", false, false, Strings.ResourceManager, new object[]
			{
				instanceName
			});
		}

		// Token: 0x17003AA5 RID: 15013
		// (get) Token: 0x0600BA97 RID: 47767 RVA: 0x002A8B5B File Offset: 0x002A6D5B
		public static LocalizedString InstallEdgeTransportServiceTask
		{
			get
			{
				return new LocalizedString("InstallEdgeTransportServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AA6 RID: 15014
		// (get) Token: 0x0600BA98 RID: 47768 RVA: 0x002A8B79 File Offset: 0x002A6D79
		public static LocalizedString EdgeTransportServiceDescription
		{
			get
			{
				return new LocalizedString("EdgeTransportServiceDescription", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AA7 RID: 15015
		// (get) Token: 0x0600BA99 RID: 47769 RVA: 0x002A8B97 File Offset: 0x002A6D97
		public static LocalizedString ServiceAlreadyInstalled
		{
			get
			{
				return new LocalizedString("ServiceAlreadyInstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AA8 RID: 15016
		// (get) Token: 0x0600BA9A RID: 47770 RVA: 0x002A8BB5 File Offset: 0x002A6DB5
		public static LocalizedString SslPortSameAsLdapPort
		{
			get
			{
				return new LocalizedString("SslPortSameAsLdapPort", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AA9 RID: 15017
		// (get) Token: 0x0600BA9B RID: 47771 RVA: 0x002A8BD3 File Offset: 0x002A6DD3
		public static LocalizedString EdgeTransportServiceNotInstalled
		{
			get
			{
				return new LocalizedString("EdgeTransportServiceNotInstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA9C RID: 47772 RVA: 0x002A8BF4 File Offset: 0x002A6DF4
		public static LocalizedString AdamUninstallGeneralFailureWithResult(int adamErrorCode)
		{
			return new LocalizedString("AdamUninstallGeneralFailureWithResult", "", false, false, Strings.ResourceManager, new object[]
			{
				adamErrorCode
			});
		}

		// Token: 0x17003AAA RID: 15018
		// (get) Token: 0x0600BA9D RID: 47773 RVA: 0x002A8C28 File Offset: 0x002A6E28
		public static LocalizedString TransportLogSearchServiceDescription
		{
			get
			{
				return new LocalizedString("TransportLogSearchServiceDescription", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA9E RID: 47774 RVA: 0x002A8C48 File Offset: 0x002A6E48
		public static LocalizedString InvalidCharsInPath(string path)
		{
			return new LocalizedString("InvalidCharsInPath", "", false, false, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x0600BA9F RID: 47775 RVA: 0x002A8C78 File Offset: 0x002A6E78
		public static LocalizedString OccupiedPortsInformation(string processOutput)
		{
			return new LocalizedString("OccupiedPortsInformation", "", false, false, Strings.ResourceManager, new object[]
			{
				processOutput
			});
		}

		// Token: 0x17003AAB RID: 15019
		// (get) Token: 0x0600BAA0 RID: 47776 RVA: 0x002A8CA7 File Offset: 0x002A6EA7
		public static LocalizedString InstallEdgeSyncServiceTask
		{
			get
			{
				return new LocalizedString("InstallEdgeSyncServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BAA1 RID: 47777 RVA: 0x002A8CC8 File Offset: 0x002A6EC8
		public static LocalizedString InvalidDriveInPath(string path)
		{
			return new LocalizedString("InvalidDriveInPath", "", false, false, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x0600BAA2 RID: 47778 RVA: 0x002A8CF8 File Offset: 0x002A6EF8
		public static LocalizedString AdamInstallError(string error)
		{
			return new LocalizedString("AdamInstallError", "", false, false, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17003AAC RID: 15020
		// (get) Token: 0x0600BAA3 RID: 47779 RVA: 0x002A8D27 File Offset: 0x002A6F27
		public static LocalizedString TransportLogSearchServiceNotUninstalled
		{
			get
			{
				return new LocalizedString("TransportLogSearchServiceNotUninstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AAD RID: 15021
		// (get) Token: 0x0600BAA4 RID: 47780 RVA: 0x002A8D45 File Offset: 0x002A6F45
		public static LocalizedString EdgeCredentialServiceDisplayName
		{
			get
			{
				return new LocalizedString("EdgeCredentialServiceDisplayName", "Ex3A1FF6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AAE RID: 15022
		// (get) Token: 0x0600BAA5 RID: 47781 RVA: 0x002A8D63 File Offset: 0x002A6F63
		public static LocalizedString UninstallAdamTask
		{
			get
			{
				return new LocalizedString("UninstallAdamTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BAA6 RID: 47782 RVA: 0x002A8D84 File Offset: 0x002A6F84
		public static LocalizedString PortIsAvailable(int port)
		{
			return new LocalizedString("PortIsAvailable", "", false, false, Strings.ResourceManager, new object[]
			{
				port
			});
		}

		// Token: 0x17003AAF RID: 15023
		// (get) Token: 0x0600BAA7 RID: 47783 RVA: 0x002A8DB8 File Offset: 0x002A6FB8
		public static LocalizedString UninstallAntispamUpdateServiceTask
		{
			get
			{
				return new LocalizedString("UninstallAntispamUpdateServiceTask", "Ex512CDC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BAA8 RID: 47784 RVA: 0x002A8DD8 File Offset: 0x002A6FD8
		public static LocalizedString AdamInstallWarning(string warning)
		{
			return new LocalizedString("AdamInstallWarning", "", false, false, Strings.ResourceManager, new object[]
			{
				warning
			});
		}

		// Token: 0x0600BAA9 RID: 47785 RVA: 0x002A8E08 File Offset: 0x002A7008
		public static LocalizedString CouldNotConnectToAdamService(string instanceName)
		{
			return new LocalizedString("CouldNotConnectToAdamService", "", false, false, Strings.ResourceManager, new object[]
			{
				instanceName
			});
		}

		// Token: 0x17003AB0 RID: 15024
		// (get) Token: 0x0600BAAA RID: 47786 RVA: 0x002A8E37 File Offset: 0x002A7037
		public static LocalizedString AdamServiceFailedToInstall
		{
			get
			{
				return new LocalizedString("AdamServiceFailedToInstall", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BAAB RID: 47787 RVA: 0x002A8E58 File Offset: 0x002A7058
		public static LocalizedString AdamUninstallProcessFailure(string processName, int exitCode)
		{
			return new LocalizedString("AdamUninstallProcessFailure", "", false, false, Strings.ResourceManager, new object[]
			{
				processName,
				exitCode
			});
		}

		// Token: 0x17003AB1 RID: 15025
		// (get) Token: 0x0600BAAC RID: 47788 RVA: 0x002A8E90 File Offset: 0x002A7090
		public static LocalizedString AntimalwareServiceDescription
		{
			get
			{
				return new LocalizedString("AntimalwareServiceDescription", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AB2 RID: 15026
		// (get) Token: 0x0600BAAD RID: 47789 RVA: 0x002A8EAE File Offset: 0x002A70AE
		public static LocalizedString EdgeSyncServiceDescription
		{
			get
			{
				return new LocalizedString("EdgeSyncServiceDescription", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AB3 RID: 15027
		// (get) Token: 0x0600BAAE RID: 47790 RVA: 0x002A8ECC File Offset: 0x002A70CC
		public static LocalizedString InstallAntimalwareServiceTask
		{
			get
			{
				return new LocalizedString("InstallAntimalwareServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BAAF RID: 47791 RVA: 0x002A8EEC File Offset: 0x002A70EC
		public static LocalizedString CouldNotGetInfoToConnectToAdamService(string instanceName)
		{
			return new LocalizedString("CouldNotGetInfoToConnectToAdamService", "", false, false, Strings.ResourceManager, new object[]
			{
				instanceName
			});
		}

		// Token: 0x17003AB4 RID: 15028
		// (get) Token: 0x0600BAB0 RID: 47792 RVA: 0x002A8F1B File Offset: 0x002A711B
		public static LocalizedString PackagePath
		{
			get
			{
				return new LocalizedString("PackagePath", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AB5 RID: 15029
		// (get) Token: 0x0600BAB1 RID: 47793 RVA: 0x002A8F39 File Offset: 0x002A7139
		public static LocalizedString FmsServiceNotInstalled
		{
			get
			{
				return new LocalizedString("FmsServiceNotInstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AB6 RID: 15030
		// (get) Token: 0x0600BAB2 RID: 47794 RVA: 0x002A8F57 File Offset: 0x002A7157
		public static LocalizedString InstallFmsServiceTask
		{
			get
			{
				return new LocalizedString("InstallFmsServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BAB3 RID: 47795 RVA: 0x002A8F78 File Offset: 0x002A7178
		public static LocalizedString InvalidServicesDependedOn(string serviceName)
		{
			return new LocalizedString("InvalidServicesDependedOn", "", false, false, Strings.ResourceManager, new object[]
			{
				serviceName
			});
		}

		// Token: 0x17003AB7 RID: 15031
		// (get) Token: 0x0600BAB4 RID: 47796 RVA: 0x002A8FA7 File Offset: 0x002A71A7
		public static LocalizedString FmsServiceDisplayName
		{
			get
			{
				return new LocalizedString("FmsServiceDisplayName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AB8 RID: 15032
		// (get) Token: 0x0600BAB5 RID: 47797 RVA: 0x002A8FC5 File Offset: 0x002A71C5
		public static LocalizedString UninstallFmsServiceTask
		{
			get
			{
				return new LocalizedString("UninstallFmsServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BAB6 RID: 47798 RVA: 0x002A8FE4 File Offset: 0x002A71E4
		public static LocalizedString PortIsBusy(int port)
		{
			return new LocalizedString("PortIsBusy", "", false, false, Strings.ResourceManager, new object[]
			{
				port
			});
		}

		// Token: 0x17003AB9 RID: 15033
		// (get) Token: 0x0600BAB7 RID: 47799 RVA: 0x002A9018 File Offset: 0x002A7218
		public static LocalizedString EdgeCredentialServiceDescription
		{
			get
			{
				return new LocalizedString("EdgeCredentialServiceDescription", "Ex682626", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003ABA RID: 15034
		// (get) Token: 0x0600BAB8 RID: 47800 RVA: 0x002A9036 File Offset: 0x002A7236
		public static LocalizedString UninstallAuditTask
		{
			get
			{
				return new LocalizedString("UninstallAuditTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BAB9 RID: 47801 RVA: 0x002A9054 File Offset: 0x002A7254
		public static LocalizedString AdamInstallProcessFailure(string processName, int exitCode)
		{
			return new LocalizedString("AdamInstallProcessFailure", "", false, false, Strings.ResourceManager, new object[]
			{
				processName,
				exitCode
			});
		}

		// Token: 0x17003ABB RID: 15035
		// (get) Token: 0x0600BABA RID: 47802 RVA: 0x002A908C File Offset: 0x002A728C
		public static LocalizedString FmsServiceNotUninstalled
		{
			get
			{
				return new LocalizedString("FmsServiceNotUninstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003ABC RID: 15036
		// (get) Token: 0x0600BABB RID: 47803 RVA: 0x002A90AA File Offset: 0x002A72AA
		public static LocalizedString InstallAntispamUpdateServiceTask
		{
			get
			{
				return new LocalizedString("InstallAntispamUpdateServiceTask", "ExDA2FEA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003ABD RID: 15037
		// (get) Token: 0x0600BABC RID: 47804 RVA: 0x002A90C8 File Offset: 0x002A72C8
		public static LocalizedString EdgeSyncServiceNotInstalled
		{
			get
			{
				return new LocalizedString("EdgeSyncServiceNotInstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BABD RID: 47805 RVA: 0x002A90E8 File Offset: 0x002A72E8
		public static LocalizedString AdamSchemaImportProcessFailure(string processName, int exitCode)
		{
			return new LocalizedString("AdamSchemaImportProcessFailure", "", false, false, Strings.ResourceManager, new object[]
			{
				processName,
				exitCode
			});
		}

		// Token: 0x0600BABE RID: 47806 RVA: 0x002A9120 File Offset: 0x002A7320
		public static LocalizedString AdamSchemaImportFailure(string error)
		{
			return new LocalizedString("AdamSchemaImportFailure", "", false, false, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17003ABE RID: 15038
		// (get) Token: 0x0600BABF RID: 47807 RVA: 0x002A914F File Offset: 0x002A734F
		public static LocalizedString InvalidLdapFileName
		{
			get
			{
				return new LocalizedString("InvalidLdapFileName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003ABF RID: 15039
		// (get) Token: 0x0600BAC0 RID: 47808 RVA: 0x002A916D File Offset: 0x002A736D
		public static LocalizedString AntimalwareServiceNotInstalled
		{
			get
			{
				return new LocalizedString("AntimalwareServiceNotInstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AC0 RID: 15040
		// (get) Token: 0x0600BAC1 RID: 47809 RVA: 0x002A918B File Offset: 0x002A738B
		public static LocalizedString AntimalwareServiceDisplayName
		{
			get
			{
				return new LocalizedString("AntimalwareServiceDisplayName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BAC2 RID: 47810 RVA: 0x002A91AC File Offset: 0x002A73AC
		public static LocalizedString AdamSetAclsProcessFailure(string processName, int exitCode, string dn)
		{
			return new LocalizedString("AdamSetAclsProcessFailure", "", false, false, Strings.ResourceManager, new object[]
			{
				processName,
				exitCode,
				dn
			});
		}

		// Token: 0x17003AC1 RID: 15041
		// (get) Token: 0x0600BAC3 RID: 47811 RVA: 0x002A91E8 File Offset: 0x002A73E8
		public static LocalizedString FmsServiceDescription
		{
			get
			{
				return new LocalizedString("FmsServiceDescription", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BAC4 RID: 47812 RVA: 0x002A9208 File Offset: 0x002A7408
		public static LocalizedString AdamUninstallWarning(string warning)
		{
			return new LocalizedString("AdamUninstallWarning", "", false, false, Strings.ResourceManager, new object[]
			{
				warning
			});
		}

		// Token: 0x0600BAC5 RID: 47813 RVA: 0x002A9238 File Offset: 0x002A7438
		public static LocalizedString ReadOnlyPath(string path)
		{
			return new LocalizedString("ReadOnlyPath", "", false, false, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x17003AC2 RID: 15042
		// (get) Token: 0x0600BAC6 RID: 47814 RVA: 0x002A9267 File Offset: 0x002A7467
		public static LocalizedString AdamServiceNotInstalled
		{
			get
			{
				return new LocalizedString("AdamServiceNotInstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AC3 RID: 15043
		// (get) Token: 0x0600BAC7 RID: 47815 RVA: 0x002A9285 File Offset: 0x002A7485
		public static LocalizedString EdgeSyncServiceDisplayName
		{
			get
			{
				return new LocalizedString("EdgeSyncServiceDisplayName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AC4 RID: 15044
		// (get) Token: 0x0600BAC8 RID: 47816 RVA: 0x002A92A3 File Offset: 0x002A74A3
		public static LocalizedString UninstallAntimalwareServiceTask
		{
			get
			{
				return new LocalizedString("UninstallAntimalwareServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AC5 RID: 15045
		// (get) Token: 0x0600BAC9 RID: 47817 RVA: 0x002A92C1 File Offset: 0x002A74C1
		public static LocalizedString InstallAdamSchemaTask
		{
			get
			{
				return new LocalizedString("InstallAdamSchemaTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AC6 RID: 15046
		// (get) Token: 0x0600BACA RID: 47818 RVA: 0x002A92DF File Offset: 0x002A74DF
		public static LocalizedString Port
		{
			get
			{
				return new LocalizedString("Port", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AC7 RID: 15047
		// (get) Token: 0x0600BACB RID: 47819 RVA: 0x002A92FD File Offset: 0x002A74FD
		public static LocalizedString EdgeCredentialServiceNotInstalled
		{
			get
			{
				return new LocalizedString("EdgeCredentialServiceNotInstalled", "Ex77C32C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AC8 RID: 15048
		// (get) Token: 0x0600BACC RID: 47820 RVA: 0x002A931B File Offset: 0x002A751B
		public static LocalizedString EdgeTransportServiceDisplayName
		{
			get
			{
				return new LocalizedString("EdgeTransportServiceDisplayName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AC9 RID: 15049
		// (get) Token: 0x0600BACD RID: 47821 RVA: 0x002A9339 File Offset: 0x002A7539
		public static LocalizedString AntimalwareServiceNotUninstalled
		{
			get
			{
				return new LocalizedString("AntimalwareServiceNotUninstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003ACA RID: 15050
		// (get) Token: 0x0600BACE RID: 47822 RVA: 0x002A9357 File Offset: 0x002A7557
		public static LocalizedString EdgeCredentialServiceNotUninstalled
		{
			get
			{
				return new LocalizedString("EdgeCredentialServiceNotUninstalled", "Ex2EADCA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003ACB RID: 15051
		// (get) Token: 0x0600BACF RID: 47823 RVA: 0x002A9375 File Offset: 0x002A7575
		public static LocalizedString InstallAdamTask
		{
			get
			{
				return new LocalizedString("InstallAdamTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003ACC RID: 15052
		// (get) Token: 0x0600BAD0 RID: 47824 RVA: 0x002A9393 File Offset: 0x002A7593
		public static LocalizedString AdamServiceDescription
		{
			get
			{
				return new LocalizedString("AdamServiceDescription", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003ACD RID: 15053
		// (get) Token: 0x0600BAD1 RID: 47825 RVA: 0x002A93B1 File Offset: 0x002A75B1
		public static LocalizedString AdamInstallFailureDataOrLogFolderNotEmpty
		{
			get
			{
				return new LocalizedString("AdamInstallFailureDataOrLogFolderNotEmpty", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003ACE RID: 15054
		// (get) Token: 0x0600BAD2 RID: 47826 RVA: 0x002A93CF File Offset: 0x002A75CF
		public static LocalizedString NoPathArgument
		{
			get
			{
				return new LocalizedString("NoPathArgument", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BAD3 RID: 47827 RVA: 0x002A93F0 File Offset: 0x002A75F0
		public static LocalizedString LogProcessExitCode(string processName, int exitCode)
		{
			return new LocalizedString("LogProcessExitCode", "", false, false, Strings.ResourceManager, new object[]
			{
				processName,
				exitCode
			});
		}

		// Token: 0x17003ACF RID: 15055
		// (get) Token: 0x0600BAD4 RID: 47828 RVA: 0x002A9428 File Offset: 0x002A7628
		public static LocalizedString TransportLogSearchServiceNotInstalled
		{
			get
			{
				return new LocalizedString("TransportLogSearchServiceNotInstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AD0 RID: 15056
		// (get) Token: 0x0600BAD5 RID: 47829 RVA: 0x002A9446 File Offset: 0x002A7646
		public static LocalizedString InstallDir
		{
			get
			{
				return new LocalizedString("InstallDir", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AD1 RID: 15057
		// (get) Token: 0x0600BAD6 RID: 47830 RVA: 0x002A9464 File Offset: 0x002A7664
		public static LocalizedString AdamServiceDisplayName
		{
			get
			{
				return new LocalizedString("AdamServiceDisplayName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BAD7 RID: 47831 RVA: 0x002A9484 File Offset: 0x002A7684
		public static LocalizedString NoPermissionsForPath(string path)
		{
			return new LocalizedString("NoPermissionsForPath", "", false, false, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x17003AD2 RID: 15058
		// (get) Token: 0x0600BAD8 RID: 47832 RVA: 0x002A94B3 File Offset: 0x002A76B3
		public static LocalizedString InvalidPackagePathValue
		{
			get
			{
				return new LocalizedString("InvalidPackagePathValue", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BAD9 RID: 47833 RVA: 0x002A94D4 File Offset: 0x002A76D4
		public static LocalizedString AdamInstallGeneralFailureWithResult(int adamErrorCode)
		{
			return new LocalizedString("AdamInstallGeneralFailureWithResult", "", false, false, Strings.ResourceManager, new object[]
			{
				adamErrorCode
			});
		}

		// Token: 0x0600BADA RID: 47834 RVA: 0x002A9508 File Offset: 0x002A7708
		public static LocalizedString LogRunningCommand(string processName, string args)
		{
			return new LocalizedString("LogRunningCommand", "", false, false, Strings.ResourceManager, new object[]
			{
				processName,
				args
			});
		}

		// Token: 0x0600BADB RID: 47835 RVA: 0x002A953C File Offset: 0x002A773C
		public static LocalizedString AdamFailedSetServiceArgs(string processName, int exitCode, string argument)
		{
			return new LocalizedString("AdamFailedSetServiceArgs", "", false, false, Strings.ResourceManager, new object[]
			{
				processName,
				exitCode,
				argument
			});
		}

		// Token: 0x17003AD3 RID: 15059
		// (get) Token: 0x0600BADC RID: 47836 RVA: 0x002A9578 File Offset: 0x002A7778
		public static LocalizedString EdgeSyncServiceNotUninstalled
		{
			get
			{
				return new LocalizedString("EdgeSyncServiceNotUninstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AD4 RID: 15060
		// (get) Token: 0x0600BADD RID: 47837 RVA: 0x002A9596 File Offset: 0x002A7796
		public static LocalizedString UninstallOldEdgeTransportServiceTask
		{
			get
			{
				return new LocalizedString("UninstallOldEdgeTransportServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BADE RID: 47838 RVA: 0x002A95B4 File Offset: 0x002A77B4
		public static LocalizedString InvalidPortNumber(int port)
		{
			return new LocalizedString("InvalidPortNumber", "", false, false, Strings.ResourceManager, new object[]
			{
				port
			});
		}

		// Token: 0x0600BADF RID: 47839 RVA: 0x002A95E8 File Offset: 0x002A77E8
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04006505 RID: 25861
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(57);

		// Token: 0x04006506 RID: 25862
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.EdgeSetupStrings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02001217 RID: 4631
		public enum IDs : uint
		{
			// Token: 0x04006508 RID: 25864
			GatewayRoleIsNotUnpacked = 774587054U,
			// Token: 0x04006509 RID: 25865
			AntispamUpdateServiceDescription = 2478794571U,
			// Token: 0x0400650A RID: 25866
			AdamServiceFailedToUninstall = 988899670U,
			// Token: 0x0400650B RID: 25867
			TransportLogSearchServiceDisplayName = 2393646509U,
			// Token: 0x0400650C RID: 25868
			UninstallEdgeTransportServiceTask = 175509160U,
			// Token: 0x0400650D RID: 25869
			UninstallEdgeSyncServiceTask = 4124898948U,
			// Token: 0x0400650E RID: 25870
			AntispamUpdateServiceDisplayName = 2736467170U,
			// Token: 0x0400650F RID: 25871
			InstallAuditTask = 1953906065U,
			// Token: 0x04006510 RID: 25872
			EdgeTransportServiceNotUninstalled = 2960500387U,
			// Token: 0x04006511 RID: 25873
			InstallEdgeTransportServiceTask = 4049824753U,
			// Token: 0x04006512 RID: 25874
			EdgeTransportServiceDescription = 2949158791U,
			// Token: 0x04006513 RID: 25875
			ServiceAlreadyInstalled = 2685419127U,
			// Token: 0x04006514 RID: 25876
			SslPortSameAsLdapPort = 576724013U,
			// Token: 0x04006515 RID: 25877
			EdgeTransportServiceNotInstalled = 159382586U,
			// Token: 0x04006516 RID: 25878
			TransportLogSearchServiceDescription = 4271619264U,
			// Token: 0x04006517 RID: 25879
			InstallEdgeSyncServiceTask = 1444113173U,
			// Token: 0x04006518 RID: 25880
			TransportLogSearchServiceNotUninstalled = 3376781594U,
			// Token: 0x04006519 RID: 25881
			EdgeCredentialServiceDisplayName = 2900475730U,
			// Token: 0x0400651A RID: 25882
			UninstallAdamTask = 767347686U,
			// Token: 0x0400651B RID: 25883
			UninstallAntispamUpdateServiceTask = 3473798678U,
			// Token: 0x0400651C RID: 25884
			AdamServiceFailedToInstall = 399958817U,
			// Token: 0x0400651D RID: 25885
			AntimalwareServiceDescription = 4113916114U,
			// Token: 0x0400651E RID: 25886
			EdgeSyncServiceDescription = 3105007247U,
			// Token: 0x0400651F RID: 25887
			InstallAntimalwareServiceTask = 683215962U,
			// Token: 0x04006520 RID: 25888
			PackagePath = 3584000439U,
			// Token: 0x04006521 RID: 25889
			FmsServiceNotInstalled = 2829653254U,
			// Token: 0x04006522 RID: 25890
			InstallFmsServiceTask = 871484781U,
			// Token: 0x04006523 RID: 25891
			FmsServiceDisplayName = 925743836U,
			// Token: 0x04006524 RID: 25892
			UninstallFmsServiceTask = 2657970248U,
			// Token: 0x04006525 RID: 25893
			EdgeCredentialServiceDescription = 2391764367U,
			// Token: 0x04006526 RID: 25894
			UninstallAuditTask = 3216105370U,
			// Token: 0x04006527 RID: 25895
			FmsServiceNotUninstalled = 292208835U,
			// Token: 0x04006528 RID: 25896
			InstallAntispamUpdateServiceTask = 2034142329U,
			// Token: 0x04006529 RID: 25897
			EdgeSyncServiceNotInstalled = 1498913134U,
			// Token: 0x0400652A RID: 25898
			InvalidLdapFileName = 140982033U,
			// Token: 0x0400652B RID: 25899
			AntimalwareServiceNotInstalled = 1307180415U,
			// Token: 0x0400652C RID: 25900
			AntimalwareServiceDisplayName = 4193735315U,
			// Token: 0x0400652D RID: 25901
			FmsServiceDescription = 1415188871U,
			// Token: 0x0400652E RID: 25902
			AdamServiceNotInstalled = 3248437725U,
			// Token: 0x0400652F RID: 25903
			EdgeSyncServiceDisplayName = 2855011956U,
			// Token: 0x04006530 RID: 25904
			UninstallAntimalwareServiceTask = 3104026373U,
			// Token: 0x04006531 RID: 25905
			InstallAdamSchemaTask = 2915042048U,
			// Token: 0x04006532 RID: 25906
			Port = 3430669213U,
			// Token: 0x04006533 RID: 25907
			EdgeCredentialServiceNotInstalled = 1957953568U,
			// Token: 0x04006534 RID: 25908
			EdgeTransportServiceDisplayName = 3698153928U,
			// Token: 0x04006535 RID: 25909
			AntimalwareServiceNotUninstalled = 1795457964U,
			// Token: 0x04006536 RID: 25910
			EdgeCredentialServiceNotUninstalled = 1454104731U,
			// Token: 0x04006537 RID: 25911
			InstallAdamTask = 2145801173U,
			// Token: 0x04006538 RID: 25912
			AdamServiceDescription = 4104091810U,
			// Token: 0x04006539 RID: 25913
			AdamInstallFailureDataOrLogFolderNotEmpty = 303188103U,
			// Token: 0x0400653A RID: 25914
			NoPathArgument = 2126302003U,
			// Token: 0x0400653B RID: 25915
			TransportLogSearchServiceNotInstalled = 2777897381U,
			// Token: 0x0400653C RID: 25916
			InstallDir = 1507908660U,
			// Token: 0x0400653D RID: 25917
			AdamServiceDisplayName = 2869926485U,
			// Token: 0x0400653E RID: 25918
			InvalidPackagePathValue = 523929713U,
			// Token: 0x0400653F RID: 25919
			EdgeSyncServiceNotUninstalled = 830945707U,
			// Token: 0x04006540 RID: 25920
			UninstallOldEdgeTransportServiceTask = 588066505U
		}

		// Token: 0x02001218 RID: 4632
		private enum ParamIDs
		{
			// Token: 0x04006542 RID: 25922
			PathTooLong,
			// Token: 0x04006543 RID: 25923
			AdamUninstallError,
			// Token: 0x04006544 RID: 25924
			InvalidAdamInstanceName,
			// Token: 0x04006545 RID: 25925
			AdamUninstallGeneralFailureWithResult,
			// Token: 0x04006546 RID: 25926
			InvalidCharsInPath,
			// Token: 0x04006547 RID: 25927
			OccupiedPortsInformation,
			// Token: 0x04006548 RID: 25928
			InvalidDriveInPath,
			// Token: 0x04006549 RID: 25929
			AdamInstallError,
			// Token: 0x0400654A RID: 25930
			PortIsAvailable,
			// Token: 0x0400654B RID: 25931
			AdamInstallWarning,
			// Token: 0x0400654C RID: 25932
			CouldNotConnectToAdamService,
			// Token: 0x0400654D RID: 25933
			AdamUninstallProcessFailure,
			// Token: 0x0400654E RID: 25934
			CouldNotGetInfoToConnectToAdamService,
			// Token: 0x0400654F RID: 25935
			InvalidServicesDependedOn,
			// Token: 0x04006550 RID: 25936
			PortIsBusy,
			// Token: 0x04006551 RID: 25937
			AdamInstallProcessFailure,
			// Token: 0x04006552 RID: 25938
			AdamSchemaImportProcessFailure,
			// Token: 0x04006553 RID: 25939
			AdamSchemaImportFailure,
			// Token: 0x04006554 RID: 25940
			AdamSetAclsProcessFailure,
			// Token: 0x04006555 RID: 25941
			AdamUninstallWarning,
			// Token: 0x04006556 RID: 25942
			ReadOnlyPath,
			// Token: 0x04006557 RID: 25943
			LogProcessExitCode,
			// Token: 0x04006558 RID: 25944
			NoPermissionsForPath,
			// Token: 0x04006559 RID: 25945
			AdamInstallGeneralFailureWithResult,
			// Token: 0x0400655A RID: 25946
			LogRunningCommand,
			// Token: 0x0400655B RID: 25947
			AdamFailedSetServiceArgs,
			// Token: 0x0400655C RID: 25948
			InvalidPortNumber
		}
	}
}
