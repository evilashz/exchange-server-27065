using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Flighting;
using Microsoft.Exchange.VariantConfiguration.DataLoad;
using Microsoft.Search.Platform.Parallax;
using Microsoft.Search.Platform.Parallax.DataLoad;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000145 RID: 325
	public class VariantConfigurationSnapshotProvider
	{
		// Token: 0x06000F00 RID: 3840 RVA: 0x00025374 File Offset: 0x00023574
		internal VariantConfigurationSnapshotProvider(IDataSourceReader dataSourceReader, IDataTransformation dataTransformation, string[] dataSourceNames, VariantConfigurationBehavior behavior, string flightingConfigFileName, string overrideAllowedTeam)
		{
			this.Behavior = behavior;
			this.DataSourceNames = dataSourceNames;
			this.flightingConfigFileName = flightingConfigFileName;
			this.overrideAllowedTeamName = overrideAllowedTeam;
			IEnumerable<string> preloadDataSources;
			if (!this.Behavior.HasFlag(VariantConfigurationBehavior.DelayLoadDataSources))
			{
				preloadDataSources = dataSourceNames;
			}
			else
			{
				List<string> list = new List<string>();
				if (this.Behavior.HasFlag(VariantConfigurationBehavior.EvaluateTeams))
				{
					list.Add(this.flightingConfigFileName);
				}
				preloadDataSources = list;
			}
			this.DataLoader = new VariantConfigurationDataLoader(dataSourceReader, dataTransformation, preloadDataSources);
			this.Container.RegisterDataLoader(this.DataLoader);
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00025418 File Offset: 0x00023618
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x00025420 File Offset: 0x00023620
		internal string[] DataSourceNames { get; private set; }

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00025429 File Offset: 0x00023629
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x00025431 File Offset: 0x00023631
		internal VariantConfigurationDataLoader DataLoader { get; private set; }

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0002543A File Offset: 0x0002363A
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x00025442 File Offset: 0x00023642
		internal VariantConfigurationBehavior Behavior { get; private set; }

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0002544C File Offset: 0x0002364C
		internal VariantObjectStoreContainer Container
		{
			get
			{
				if (this.container == null)
				{
					lock (this.containerLock)
					{
						if (this.container == null)
						{
							this.container = VariantObjectStoreContainerFactory.Default.Create();
						}
					}
				}
				return this.container;
			}
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x000254AC File Offset: 0x000236AC
		public VariantConfigurationSnapshot GetSnapshot(string rotationId, string rampId, ConstraintCollection constraints, IEnumerable<string> overrideFlights = null)
		{
			if (constraints == null)
			{
				throw new ArgumentNullException("constraints");
			}
			int rotationHash;
			if (string.IsNullOrWhiteSpace(rotationId))
			{
				rotationHash = -1;
			}
			else
			{
				rotationHash = RotationHash.ComputeHash(rotationId);
			}
			VariantObjectStore currentSnapshot = this.Container.GetCurrentSnapshot();
			currentSnapshot.DefaultContext.InitializeFrom(constraints);
			if (this.Behavior.HasFlag(VariantConfigurationBehavior.EvaluateTeams))
			{
				this.AddTeamsToStoreContext(currentSnapshot);
			}
			bool flag = this.Behavior.HasFlag(VariantConfigurationBehavior.EvaluateFlights);
			if (flag)
			{
				if (overrideFlights != null && this.IsUserAllowedOverride(currentSnapshot))
				{
					flag = false;
					using (IEnumerator<string> enumerator = overrideFlights.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string str = enumerator.Current;
							currentSnapshot.DefaultContext.AddVariant("flt." + str, bool.TrueString);
						}
						goto IL_D6;
					}
				}
				this.AddFlightsToStoreContext(currentSnapshot, currentSnapshot.DataSourceNames, rotationHash, rampId);
			}
			IL_D6:
			return new VariantConfigurationSnapshot(currentSnapshot, rotationHash, rampId, flag, this);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x000255E0 File Offset: 0x000237E0
		internal void AddFlightsToStoreContext(VariantObjectStore store, IEnumerable<string> dataSourceNames, int rotationHash, string rampId)
		{
			this.AddFlightsToStoreContext(store, dataSourceNames, (IFlight flight) => this.Rotate(rotationHash, flight) && this.Ramp(rampId, flight));
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00025620 File Offset: 0x00023820
		internal bool Ramp(string rampId, IFlight flight)
		{
			if (string.IsNullOrWhiteSpace(flight.Ramp))
			{
				return true;
			}
			int hash;
			if (string.IsNullOrWhiteSpace(rampId) || string.Equals(rampId, MachineSettingsContext.Local.RampId))
			{
				hash = -1;
			}
			else
			{
				hash = this.ComputeHashWithSeed(rampId, flight.RampSeed);
			}
			return this.IsHashInRange(hash, flight.Ramp, flight.Name);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0002567B File Offset: 0x0002387B
		internal bool Rotate(int rotationHash, IFlight flight)
		{
			return this.IsHashInRange(rotationHash, flight.Rotate, flight.Name);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00025690 File Offset: 0x00023890
		internal int ComputeHashWithSeed(string id, string seed)
		{
			if (string.IsNullOrWhiteSpace(seed))
			{
				seed = string.Empty;
			}
			return RotationHash.ComputeHash(seed + id);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x000256B0 File Offset: 0x000238B0
		private bool IsHashInRange(int hash, string range, string flightName)
		{
			if (string.IsNullOrWhiteSpace(range))
			{
				return true;
			}
			if (hash == -1)
			{
				return false;
			}
			range = range.Trim();
			if (range.Equals("norotate", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			string text = null;
			try
			{
				if (range.EndsWith("%"))
				{
					if (range.Equals("100%"))
					{
						return true;
					}
					text = range.Substring(0, range.Length - 1);
					int num = (int)(float.Parse(text) * 10f);
					return hash < num;
				}
				else
				{
					foreach (string text2 in range.Split(new char[]
					{
						':'
					}))
					{
						string[] array2 = text2.Split(new char[]
						{
							','
						});
						text = array2[0];
						int num2 = int.Parse(text);
						text = array2[1];
						int num3 = int.Parse(text);
						if (num2 <= hash && hash <= num3)
						{
							return true;
						}
					}
				}
			}
			catch (FormatException innerException)
			{
				throw new FormatException(string.Format("Failed to parse Rotate or Ramp value '{0}' for flight {1}. The following part of the value is malformed: '{2}'.", range, flightName, text), innerException);
			}
			return false;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x000257D0 File Offset: 0x000239D0
		private bool IsUserAllowedOverride(VariantObjectStore store)
		{
			string text = "team." + this.overrideAllowedTeamName;
			string[] variantValues = store.DefaultContext.GetVariantValues(text);
			bool flag;
			return variantValues.Length > 0 && bool.TryParse(variantValues.First<string>(), out flag) && flag;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00025814 File Offset: 0x00023A14
		private void AddTeamsToStoreContext(VariantObjectStore store)
		{
			VariantObjectProvider resolvedObjectProvider = store.GetResolvedObjectProvider(this.flightingConfigFileName);
			foreach (KeyValuePair<string, ITeam> keyValuePair in resolvedObjectProvider.GetObjectsOfType<ITeam>())
			{
				if (keyValuePair.Value.IsMember)
				{
					store.DefaultContext.AddVariant("team." + keyValuePair.Value.Name, bool.TrueString);
				}
			}
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0002589C File Offset: 0x00023A9C
		private void AddFlightsToStoreContext(VariantObjectStore store, IEnumerable<string> dataSourceNames, Func<IFlight, bool> isUserInFlightPercentage)
		{
			foreach (string path in dataSourceNames)
			{
				try
				{
					VariantObjectProvider resolvedObjectProvider = store.GetResolvedObjectProvider(Path.GetFileName(path));
					foreach (KeyValuePair<string, IFlight> keyValuePair in resolvedObjectProvider.GetObjectsOfType<IFlight>())
					{
						if (keyValuePair.Value.Enabled && isUserInFlightPercentage(keyValuePair.Value))
						{
							store.DefaultContext.AddVariant(keyValuePair.Value.Name, bool.TrueString);
						}
					}
				}
				catch (DataSourceNotFoundException)
				{
				}
			}
		}

		// Token: 0x040004F9 RID: 1273
		private const string FlightPrefix = "flt.";

		// Token: 0x040004FA RID: 1274
		private const string FlightFileSuffix = ".flight.ini";

		// Token: 0x040004FB RID: 1275
		private const string NoRotationValue = "norotate";

		// Token: 0x040004FC RID: 1276
		private const string TeamPrefix = "team.";

		// Token: 0x040004FD RID: 1277
		private readonly string overrideAllowedTeamName;

		// Token: 0x040004FE RID: 1278
		private readonly string flightingConfigFileName;

		// Token: 0x040004FF RID: 1279
		private readonly object containerLock = new object();

		// Token: 0x04000500 RID: 1280
		private VariantObjectStoreContainer container;
	}
}
