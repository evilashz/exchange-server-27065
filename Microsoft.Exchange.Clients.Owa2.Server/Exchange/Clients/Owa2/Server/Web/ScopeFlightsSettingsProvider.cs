using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200048C RID: 1164
	public class ScopeFlightsSettingsProvider
	{
		// Token: 0x0600279B RID: 10139 RVA: 0x00092B24 File Offset: 0x00090D24
		public ScopeFlightsSettingsProvider()
		{
			this.variantConfigurationSnapshotFactory = delegate(string scope)
			{
				ScopeFlightsSettingsProvider.ScopeConstraintProvider constraintProvider = new ScopeFlightsSettingsProvider.ScopeConstraintProvider(scope);
				return VariantConfiguration.GetSnapshot(constraintProvider, null, null);
			};
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x00092B4F File Offset: 0x00090D4F
		public static IReadOnlyList<string> LogicalScopes
		{
			get
			{
				return ScopeFlightsSettingsProvider.logicalScopes;
			}
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x00092B56 File Offset: 0x00090D56
		public ScopeFlightsSettingsProvider(Func<string, VariantConfigurationSnapshot> variantConfigurationSnapshotFactory)
		{
			this.variantConfigurationSnapshotFactory = variantConfigurationSnapshotFactory;
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x00092B65 File Offset: 0x00090D65
		public static bool IsLogicalScope(string scope)
		{
			return ScopeFlightsSettingsProvider.LogicalScopes.Contains(scope) || scope.StartsWith("team.");
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x00092B84 File Offset: 0x00090D84
		public IList<ScopeFlightsSetting> GetFlightsForScope()
		{
			List<ScopeFlightsSetting> list = new List<ScopeFlightsSetting>();
			for (int i = 0; i < ScopeFlightsSettingsProvider.LogicalScopes.Count; i++)
			{
				string scope = ScopeFlightsSettingsProvider.LogicalScopes[i];
				string[] flightsForScope = this.GetFlightsForScope(scope);
				list.Add(new ScopeFlightsSetting(scope, flightsForScope));
			}
			return list;
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x00092BD0 File Offset: 0x00090DD0
		public string[] GetFlightsForScope(string scope)
		{
			VariantConfigurationSnapshot variantConfigurationSnapshot = this.variantConfigurationSnapshotFactory(scope);
			return variantConfigurationSnapshot.Flights;
		}

		// Token: 0x0400170B RID: 5899
		private static readonly IReadOnlyList<string> logicalScopes = new string[]
		{
			"WorldWide",
			"Microsoft",
			"Dogfood",
			"team.GuestAccess",
			"team.OWA",
			"team.Compass"
		};

		// Token: 0x0400170C RID: 5900
		private readonly Func<string, VariantConfigurationSnapshot> variantConfigurationSnapshotFactory;

		// Token: 0x0200048D RID: 1165
		private class ScopeConstraintProvider : IConstraintProvider
		{
			// Token: 0x060027A3 RID: 10147 RVA: 0x00092C3C File Offset: 0x00090E3C
			public ScopeConstraintProvider(string scope)
			{
				this.constraints = ConstraintCollection.CreateEmpty();
				if (!scope.Equals("WorldWide", StringComparison.CurrentCultureIgnoreCase))
				{
					this.constraints.Add(VariantType.Organization, "Microsoft");
					if (!scope.Equals("Microsoft", StringComparison.CurrentCultureIgnoreCase))
					{
						this.constraints.Add(scope, true);
					}
				}
			}

			// Token: 0x17000A83 RID: 2691
			// (get) Token: 0x060027A4 RID: 10148 RVA: 0x00092C98 File Offset: 0x00090E98
			public ConstraintCollection Constraints
			{
				get
				{
					return this.constraints;
				}
			}

			// Token: 0x17000A84 RID: 2692
			// (get) Token: 0x060027A5 RID: 10149 RVA: 0x00092CA0 File Offset: 0x00090EA0
			public string RampId
			{
				get
				{
					return "ScopeConstraints";
				}
			}

			// Token: 0x17000A85 RID: 2693
			// (get) Token: 0x060027A6 RID: 10150 RVA: 0x00092CA7 File Offset: 0x00090EA7
			public string RotationId
			{
				get
				{
					return "ScopeConstraints";
				}
			}

			// Token: 0x0400170E RID: 5902
			private const string ScopeConstraintsId = "ScopeConstraints";

			// Token: 0x0400170F RID: 5903
			private readonly ConstraintCollection constraints;
		}
	}
}
