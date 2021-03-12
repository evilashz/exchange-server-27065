using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.VariantConfiguration.DataLoad;
using Microsoft.Exchange.VariantConfiguration.Parser;
using Microsoft.Exchange.VariantConfiguration.Reflection;
using Microsoft.Search.Platform.Parallax.DataLoad;
using Microsoft.Win32;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200013F RID: 319
	public static class VariantConfiguration
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000EAA RID: 3754 RVA: 0x00023D18 File Offset: 0x00021F18
		// (remove) Token: 0x06000EAB RID: 3755 RVA: 0x00023D4C File Offset: 0x00021F4C
		public static event EventHandler<UpdateCommittedEventArgs> UpdateCommitted;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000EAC RID: 3756 RVA: 0x00023D80 File Offset: 0x00021F80
		// (remove) Token: 0x06000EAD RID: 3757 RVA: 0x00023DB4 File Offset: 0x00021FB4
		internal static event EventHandler<OverridesChangedEventArgs> OverridesChanged;

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00023DE8 File Offset: 0x00021FE8
		public static VariantConfigurationSnapshot InvariantNoFlightingSnapshot
		{
			get
			{
				if (VariantConfiguration.invariantNoFlightingSnapshot == null)
				{
					lock (VariantConfiguration.StaticLock)
					{
						if (VariantConfiguration.invariantNoFlightingSnapshot == null)
						{
							VariantConfiguration.invariantNoFlightingSnapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
						}
					}
				}
				return VariantConfiguration.invariantNoFlightingSnapshot;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00023E48 File Offset: 0x00022048
		// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x00023E4F File Offset: 0x0002204F
		public static VariantConfigurationOverride[] Overrides { get; private set; } = new VariantConfigurationOverride[0];

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x00023E57 File Offset: 0x00022057
		public static VariantConfigurationSettings Settings
		{
			get
			{
				return VariantConfiguration.settings;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x00023E5E File Offset: 0x0002205E
		public static VariantConfigurationFlights Flights
		{
			get
			{
				return VariantConfiguration.flights;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x00023E65 File Offset: 0x00022065
		// (set) Token: 0x06000EB4 RID: 3764 RVA: 0x00023E6C File Offset: 0x0002206C
		internal static Func<string, string, ISettings> TestOverride { get; set; }

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00023E74 File Offset: 0x00022074
		internal static ResourcesDataSourceReader ResourcesDataSourceReader
		{
			get
			{
				if (VariantConfiguration.resourcesDataSourceReader == null)
				{
					lock (VariantConfiguration.StaticLock)
					{
						if (VariantConfiguration.resourcesDataSourceReader == null)
						{
							VariantConfiguration.resourcesDataSourceReader = new ResourcesDataSourceReader(Assembly.GetExecutingAssembly());
						}
					}
				}
				return VariantConfiguration.resourcesDataSourceReader;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00023F38 File Offset: 0x00022138
		private static VariantConfigurationSnapshotProvider Instance
		{
			get
			{
				if (VariantConfiguration.instance == null)
				{
					lock (VariantConfiguration.StaticLock)
					{
						if (VariantConfiguration.instance == null)
						{
							VariantConfigurationBehavior variantConfigurationBehavior = VariantConfigurationBehavior.DelayLoadDataSources;
							ChainedDataSourceReader chainedDataSourceReader = new ChainedDataSourceReader();
							IEnumerable<string> enumerable = VariantConfiguration.ResourcesDataSourceReader.ResourceNames;
							bool flag2 = false;
							string text = null;
							string[] second;
							if (VariantConfiguration.IsProcessAllowedToReadFiles && VariantConfiguration.TryGetConfigFolder(out text) && !string.IsNullOrEmpty(text) && VariantConfiguration.TryGetConfigFiles(text, out second))
							{
								flag2 = true;
								variantConfigurationBehavior = (variantConfigurationBehavior | VariantConfigurationBehavior.EvaluateFlights | VariantConfigurationBehavior.EvaluateTeams);
								chainedDataSourceReader.AddDataSourceReader(new FilesDataSourceReader(text));
								enumerable = enumerable.Union(second);
							}
							chainedDataSourceReader.AddDataSourceReader(VariantConfiguration.ResourcesDataSourceReader);
							if (flag2)
							{
								VariantConfiguration.changeAccumulator = new FileChangesAccumulator(text, "*.ini", 1000, false);
								VariantConfiguration.changeAccumulator.ChangesAccumulated += delegate(object sender, IEnumerable<string> args)
								{
									VariantConfiguration.ReloadChangedDatasources(args);
								};
								VariantConfiguration.changeAccumulator.ErrorDetected += delegate(object sender, Exception args)
								{
									VariantConfiguration.ReloadDatasources();
								};
							}
							OverrideDataTransformation overrideDataTransformation = OverrideDataTransformation.Get(null);
							FlightReader flightReader = new FlightReader(chainedDataSourceReader, overrideDataTransformation, enumerable);
							VariantConfiguration.OverridesChanged += delegate(object sender, OverridesChangedEventArgs overrides)
							{
								VariantConfiguration.ReloadDatasources();
							};
							VariantConfiguration.UpdateCommitted += delegate(object sender, UpdateCommittedEventArgs args)
							{
								lock (VariantConfiguration.StaticLock)
								{
									VariantConfiguration.localMachineSnapshot = null;
								}
							};
							IDataTransformation dataTransformation = new ChainedTransformation(new IDataTransformation[]
							{
								overrideDataTransformation,
								new FlightDependencyTransformation(flightReader)
							});
							VariantConfigurationSnapshotProvider variantConfigurationSnapshotProvider = new VariantConfigurationSnapshotProvider(chainedDataSourceReader, dataTransformation, (from name in enumerable
							where name.EndsWith(".settings.ini", StringComparison.OrdinalIgnoreCase)
							select name).ToArray<string>(), variantConfigurationBehavior, VariantConfiguration.FlightingConfigFileName, "GuestAccess");
							variantConfigurationSnapshotProvider.Container.DataLoadCommitted += VariantConfiguration.OnDataLoadCommitted;
							VariantConfiguration.instance = variantConfigurationSnapshotProvider;
						}
					}
				}
				return VariantConfiguration.instance;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0002413C File Offset: 0x0002233C
		private static bool IsProcessAllowedToReadFiles
		{
			get
			{
				if (VariantConfiguration.isProcessAllowedToReadFiles == null)
				{
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						VariantConfiguration.isProcessAllowedToReadFiles = new bool?(!string.Equals(ConstraintCollection.Mode, "enterprise") && !currentProcess.ProcessName.StartsWith("ExSetup", StringComparison.OrdinalIgnoreCase) && !currentProcess.ProcessName.StartsWith("Setup", StringComparison.OrdinalIgnoreCase));
					}
				}
				return VariantConfiguration.isProcessAllowedToReadFiles.Value;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x000241C8 File Offset: 0x000223C8
		private static string FlightingConfigFileName
		{
			get
			{
				if (VariantConfiguration.flightingConfigFileName == null)
				{
					VariantConfiguration.flightingConfigFileName = "Flighting.settings.ini";
				}
				return VariantConfiguration.flightingConfigFileName;
			}
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x000241E0 File Offset: 0x000223E0
		public static void Initialize(string customConfigPath)
		{
			VariantConfiguration.configPath = customConfigPath;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x000241E8 File Offset: 0x000223E8
		public static VariantConfigurationSnapshot GetSnapshot(IConstraintProvider constraintProvider, ConstraintCollection additionalConstraints = null, IEnumerable<string> overrideFlights = null)
		{
			if (constraintProvider == null)
			{
				throw new ArgumentNullException("constraintProvider");
			}
			if (constraintProvider == MachineSettingsContext.Local && additionalConstraints == null && overrideFlights == null)
			{
				VariantConfigurationSnapshot variantConfigurationSnapshot = VariantConfiguration.localMachineSnapshot;
				if (variantConfigurationSnapshot == null)
				{
					lock (VariantConfiguration.StaticLock)
					{
						if (VariantConfiguration.localMachineSnapshot == null)
						{
							VariantConfiguration.localMachineSnapshot = VariantConfiguration.Instance.GetSnapshot(constraintProvider.RotationId, constraintProvider.RampId, constraintProvider.Constraints, overrideFlights);
						}
						variantConfigurationSnapshot = VariantConfiguration.localMachineSnapshot;
					}
				}
				return variantConfigurationSnapshot;
			}
			ConstraintCollection constraintCollection = constraintProvider.Constraints;
			if (additionalConstraints != null)
			{
				constraintCollection = new ConstraintCollection(constraintCollection);
				constraintCollection.Add(additionalConstraints);
			}
			return VariantConfiguration.Instance.GetSnapshot(constraintProvider.RotationId, constraintProvider.RampId, constraintCollection, overrideFlights);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x000242B8 File Offset: 0x000224B8
		public static VariantConfigurationSnapshotProvider CreateSnapshotProvider(string folder, VariantConfigurationBehavior behavior)
		{
			if (!Directory.Exists(folder))
			{
				throw new ArgumentException(string.Format("Directory '{0}' does not exist.", folder));
			}
			IDataSourceReader dataSourceReader = new FilesDataSourceReader(folder);
			OverrideDataTransformation overrideDataTransformation = OverrideDataTransformation.Get(null);
			string[] configFiles = VariantConfiguration.GetConfigFiles(folder);
			FlightReader flightReader = new FlightReader(dataSourceReader, overrideDataTransformation, configFiles);
			IDataTransformation dataTransformation = new ChainedTransformation(new IDataTransformation[]
			{
				overrideDataTransformation,
				new FlightDependencyTransformation(flightReader)
			});
			string[] dataSourceNames = (from file in configFiles
			where file.EndsWith(".settings.ini", StringComparison.OrdinalIgnoreCase)
			select file).ToArray<string>();
			return new VariantConfigurationSnapshotProvider(dataSourceReader, dataTransformation, dataSourceNames, behavior, VariantConfiguration.FlightingConfigFileName, "GuestAccess");
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00024374 File Offset: 0x00022574
		public static ConfigurationParser CreateConfigurationParser(string folder)
		{
			if (!Directory.Exists(folder))
			{
				throw new ArgumentException(string.Format("Directory '{0}' does not exist.", folder));
			}
			return ConfigurationParser.Create(from file in VariantConfiguration.GetConfigFiles(folder)
			select Path.Combine(folder, file));
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x000243D4 File Offset: 0x000225D4
		public static bool SetOverrides(VariantConfigurationOverride[] overrides)
		{
			if (overrides == null)
			{
				overrides = new VariantConfigurationOverride[0];
			}
			if (VariantConfiguration.MatchCurrentOverrides(overrides))
			{
				return false;
			}
			VariantConfiguration.Overrides = overrides;
			EventHandler<OverridesChangedEventArgs> overridesChanged = VariantConfiguration.OverridesChanged;
			if (overridesChanged != null)
			{
				Interlocked.Add(ref VariantConfiguration.updateCommittedAccumulationCounter, VariantConfiguration.OverridesChanged.GetInvocationList().Length);
				overridesChanged(null, new OverridesChangedEventArgs(overrides));
			}
			return true;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0002442C File Offset: 0x0002262C
		internal static void OnDataLoadCommitted(object sender, TransactionCommittedEventArgs args)
		{
			VariantConfiguration.updateCommittedAccumulationTimer.Change(1000, -1);
			int num = Interlocked.Decrement(ref VariantConfiguration.updateCommittedAccumulationCounter);
			if (num == 0)
			{
				VariantConfiguration.RaiseUpdateCommitted();
				return;
			}
			if (num < 0)
			{
				Interlocked.CompareExchange(ref VariantConfiguration.updateCommittedAccumulationCounter, 0, num);
			}
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00024470 File Offset: 0x00022670
		internal static void TestInitialize(string folder, string file)
		{
			VariantConfiguration.TestInitialize(folder, new string[]
			{
				file
			}, file);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x000244A0 File Offset: 0x000226A0
		internal static void TestInitialize(string folder, string[] files, string flightingConfigFileName)
		{
			if (!files.Contains(flightingConfigFileName))
			{
				throw new ArgumentException("flightingConfigFileName must be in files");
			}
			VariantConfiguration.TestInitialize(folder, flightingConfigFileName, (string configPath) => files);
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x000244E6 File Offset: 0x000226E6
		internal static void TestInitialize(string folder)
		{
			VariantConfiguration.TestInitialize(folder, "Flighting.settings.ini", null);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x000244F4 File Offset: 0x000226F4
		internal static void TestReset()
		{
			lock (VariantConfiguration.StaticLock)
			{
				VariantConfiguration.instance = null;
				if (VariantConfiguration.changeAccumulator != null)
				{
					VariantConfiguration.changeAccumulator.Dispose();
					VariantConfiguration.changeAccumulator = null;
				}
			}
			VariantConfiguration.getConfigFilesImplementationForTesting = null;
			VariantConfiguration.flightingConfigFileName = null;
			VariantConfiguration.isProcessAllowedToReadFiles = null;
			VariantConfiguration.configPath = null;
			VariantConfiguration.localMachineSnapshot = null;
			VariantConfiguration.invariantNoFlightingSnapshot = null;
			VariantConfiguration.resourcesDataSourceReader = null;
			VariantConfiguration.OverridesChanged = null;
			VariantConfiguration.TestOverride = null;
			VariantConfiguration.UpdateCommitted = null;
			VariantConfiguration.SetOverrides(null);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00024594 File Offset: 0x00022794
		internal static VariantConfigurationSnapshotProvider GetProviderForValidation(VariantConfigurationOverride validationOverride)
		{
			if (validationOverride == null)
			{
				throw new ArgumentNullException("validationOverride");
			}
			VariantConfigurationBehavior behavior = VariantConfigurationBehavior.Default;
			ChainedDataSourceReader chainedDataSourceReader = new ChainedDataSourceReader();
			string text;
			if (VariantConfiguration.IsProcessAllowedToReadFiles && VariantConfiguration.TryGetConfigFolder(out text) && !string.IsNullOrEmpty(text))
			{
				chainedDataSourceReader.AddDataSourceReader(new FilesDataSourceReader(text));
			}
			chainedDataSourceReader.AddDataSourceReader(VariantConfiguration.ResourcesDataSourceReader);
			return new VariantConfigurationSnapshotProvider(chainedDataSourceReader, OverrideDataTransformation.Get(validationOverride), new string[]
			{
				validationOverride.FileName
			}, behavior, VariantConfiguration.FlightingConfigFileName, "GuestAccess");
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00024618 File Offset: 0x00022818
		internal static string[] GetConfigFiles(string folder)
		{
			if (VariantConfiguration.getConfigFilesImplementationForTesting != null)
			{
				return VariantConfiguration.getConfigFilesImplementationForTesting(folder);
			}
			return (from path in Directory.EnumerateFiles(folder, "*.ini")
			select Path.GetFileName(path)).ToArray<string>();
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0002466C File Offset: 0x0002286C
		internal static string GetConfigFolder()
		{
			if (VariantConfiguration.configPath == null)
			{
				string str;
				if (!VariantConfiguration.TryGetInstallFolder(out str))
				{
					throw new InvalidOperationException(string.Format("Registry entry {0}\\{1} does not exist.  VariantConfiguration is unable to locate its configuration files.", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiInstallPath"));
				}
				VariantConfiguration.configPath = str + "Config";
			}
			return VariantConfiguration.configPath;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x000246BC File Offset: 0x000228BC
		internal static bool TryGetConfigFolder(out string configFolder)
		{
			if (VariantConfiguration.configPath == null)
			{
				string str;
				if (!VariantConfiguration.TryGetInstallFolder(out str))
				{
					configFolder = null;
					return false;
				}
				VariantConfiguration.configPath = str + "Config";
			}
			configFolder = VariantConfiguration.configPath;
			return true;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x000246F8 File Offset: 0x000228F8
		private static bool TryGetInstallFolder(out string installFolder)
		{
			bool result;
			try
			{
				object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiInstallPath", null);
				if (value == null)
				{
					installFolder = null;
					result = false;
				}
				else
				{
					installFolder = (string)value;
					result = true;
				}
			}
			catch (Exception)
			{
				installFolder = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00024748 File Offset: 0x00022948
		private static bool TryGetConfigFiles(string folder, out string[] configFiles)
		{
			bool result;
			try
			{
				if (Directory.Exists(folder))
				{
					configFiles = VariantConfiguration.GetConfigFiles(folder);
					result = true;
				}
				else
				{
					configFiles = null;
					result = false;
				}
			}
			catch (Exception)
			{
				configFiles = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0002478C File Offset: 0x0002298C
		private static bool MatchCurrentOverrides(VariantConfigurationOverride[] newOverrides)
		{
			VariantConfigurationOverride[] overrides = VariantConfiguration.Overrides;
			if (overrides == null && newOverrides == null)
			{
				return true;
			}
			if (overrides == null || newOverrides == null)
			{
				return false;
			}
			if (overrides.Length != newOverrides.Length)
			{
				return false;
			}
			for (int i = 0; i < overrides.Length; i++)
			{
				if (!overrides[i].Equals(newOverrides[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x000247D8 File Offset: 0x000229D8
		private static void RaiseUpdateCommitted()
		{
			VariantConfiguration.updateCommittedAccumulationTimer.Change(-1, -1);
			Interlocked.Exchange(ref VariantConfiguration.updateCommittedAccumulationCounter, 0);
			EventHandler<UpdateCommittedEventArgs> updateCommitted = VariantConfiguration.UpdateCommitted;
			if (updateCommitted != null)
			{
				updateCommitted(null, new UpdateCommittedEventArgs());
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00024813 File Offset: 0x00022A13
		private static void ReloadDatasources()
		{
			VariantConfiguration.Instance.DataLoader.ReloadIfLoaded();
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0002483C File Offset: 0x00022A3C
		private static void ReloadChangedDatasources(IEnumerable<string> args)
		{
			IEnumerable<string> enumerable = from path in args
			select Path.GetFileName(path);
			if (enumerable.Any((string file) => file.EndsWith(".flight.ini", StringComparison.OrdinalIgnoreCase)))
			{
				VariantConfiguration.ReloadDatasources();
				return;
			}
			IEnumerable<string> dataSources = enumerable.Intersect(VariantConfiguration.Instance.DataSourceNames, StringComparer.OrdinalIgnoreCase);
			VariantConfiguration.Instance.DataLoader.ReloadIfLoaded(dataSources);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x000248C0 File Offset: 0x00022AC0
		private static void TestInitialize(string configPath, string flightingConfigFileName, Func<string, string[]> getConfigFilesImplementation)
		{
			lock (VariantConfiguration.StaticLock)
			{
				VariantConfiguration.TestReset();
				VariantConfiguration.isProcessAllowedToReadFiles = new bool?(true);
				VariantConfiguration.configPath = configPath;
				VariantConfiguration.flightingConfigFileName = flightingConfigFileName;
				VariantConfiguration.getConfigFilesImplementationForTesting = getConfigFilesImplementation;
			}
		}

		// Token: 0x040004BB RID: 1211
		public const string GlobalSnapshotId = "Global";

		// Token: 0x040004BC RID: 1212
		internal const int UpdateCommittedAccumlationMs = 1000;

		// Token: 0x040004BD RID: 1213
		internal const string DefaultFlightingConfigFileName = "Flighting.settings.ini";

		// Token: 0x040004BE RID: 1214
		internal const string DefaultOverrideAllowedTeamName = "GuestAccess";

		// Token: 0x040004BF RID: 1215
		private const string SetupInstallKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";

		// Token: 0x040004C0 RID: 1216
		private const string InstallPathName = "MsiInstallPath";

		// Token: 0x040004C1 RID: 1217
		private const string ConfigRelativePath = "Config";

		// Token: 0x040004C2 RID: 1218
		private const string ConfigWildcard = "*.ini";

		// Token: 0x040004C3 RID: 1219
		private const string SettingsFileSuffix = ".settings.ini";

		// Token: 0x040004C4 RID: 1220
		private const string FlightFileSuffix = ".flight.ini";

		// Token: 0x040004C5 RID: 1221
		private const int DefaultAccumulationTimeoutInMs = 1000;

		// Token: 0x040004C6 RID: 1222
		private static readonly object StaticLock = new object();

		// Token: 0x040004C7 RID: 1223
		private static Func<string, string[]> getConfigFilesImplementationForTesting = null;

		// Token: 0x040004C8 RID: 1224
		private static string flightingConfigFileName = null;

		// Token: 0x040004C9 RID: 1225
		private static VariantConfigurationSnapshotProvider instance = null;

		// Token: 0x040004CA RID: 1226
		private static FileChangesAccumulator changeAccumulator = null;

		// Token: 0x040004CB RID: 1227
		private static string configPath = null;

		// Token: 0x040004CC RID: 1228
		private static VariantConfigurationSnapshot invariantNoFlightingSnapshot;

		// Token: 0x040004CD RID: 1229
		private static ResourcesDataSourceReader resourcesDataSourceReader;

		// Token: 0x040004CE RID: 1230
		private static bool? isProcessAllowedToReadFiles = null;

		// Token: 0x040004CF RID: 1231
		private static VariantConfigurationSettings settings = new VariantConfigurationSettings();

		// Token: 0x040004D0 RID: 1232
		private static VariantConfigurationFlights flights = new VariantConfigurationFlights();

		// Token: 0x040004D1 RID: 1233
		private static VariantConfigurationSnapshot localMachineSnapshot = null;

		// Token: 0x040004D2 RID: 1234
		private static Timer updateCommittedAccumulationTimer = new Timer(delegate(object state)
		{
			VariantConfiguration.RaiseUpdateCommitted();
		});

		// Token: 0x040004D3 RID: 1235
		private static int updateCommittedAccumulationCounter = 0;
	}
}
