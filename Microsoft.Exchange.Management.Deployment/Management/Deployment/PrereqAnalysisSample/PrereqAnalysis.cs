using System;
using Microsoft.Exchange.Management.Analysis;
using Microsoft.Exchange.Management.Deployment.Analysis;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment.PrereqAnalysisSample
{
	// Token: 0x0200006E RID: 110
	internal class PrereqAnalysis : AnalysisImplementation<IDataProviderFactory, PrereqAnalysisMemberBuilder, PrereqConclusionSetBuilder, PrereqConclusionSet, PrereqConclusion, PrereqSettingConclusion, PrereqRuleConclusion>
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x00025E80 File Offset: 0x00024080
		public PrereqAnalysis(IDataProviderFactory dataSourceProvider, PrereqAnalysisMemberBuilder memberBuilder, PrereqConclusionSetBuilder conclusionSetBuilder, SetupMode setupModes, SetupRole setupRoles, GlobalParameters prereqAnalysisParameters, AnalysisThreading threadMode) : base(dataSourceProvider, memberBuilder, conclusionSetBuilder, (Microsoft.Exchange.Management.Deployment.Analysis.AnalysisMember x) => (!x.Features.HasFeature<ModeFeature>() || x.Features.GetFeature<ModeFeature>().Contains(setupModes)) && (!x.Features.HasFeature<RoleFeature>() || x.Features.GetFeature<RoleFeature>().Contains(setupRoles)), (Microsoft.Exchange.Management.Deployment.Analysis.AnalysisMember x) => (!x.Features.HasFeature<ModeFeature>() || x.Features.GetFeature<ModeFeature>().Contains(setupModes)) && (!x.Features.HasFeature<RoleFeature>() || x.Features.GetFeature<RoleFeature>().Contains(setupRoles)), threadMode)
		{
			this.setupModes = setupModes;
			this.setupRoles = setupRoles;
			this.prereqAnalysisParameters = prereqAnalysisParameters;
			this.BuildPrereqSettings();
			this.BuildPrereqRules();
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00025F00 File Offset: 0x00024100
		public SetupMode SetupModes
		{
			get
			{
				return this.setupModes;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00025F08 File Offset: 0x00024108
		public SetupRole SetupRoles
		{
			get
			{
				return this.setupRoles;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00025F10 File Offset: 0x00024110
		public GlobalParameters PrereqAnalysisParameters
		{
			get
			{
				return this.prereqAnalysisParameters;
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00025F18 File Offset: 0x00024118
		protected override void OnAnalysisStart()
		{
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00025F1A File Offset: 0x0002411A
		protected override void OnAnalysisStop()
		{
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00025F1C File Offset: 0x0002411C
		protected override void OnAnalysisMemberStart(Microsoft.Exchange.Management.Deployment.Analysis.AnalysisMember member)
		{
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00025F1E File Offset: 0x0002411E
		protected override void OnAnalysisMemberStop(Microsoft.Exchange.Management.Deployment.Analysis.AnalysisMember member)
		{
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00025F20 File Offset: 0x00024120
		protected override void OnAnalysisMemberEvaluate(Microsoft.Exchange.Management.Deployment.Analysis.AnalysisMember member, Microsoft.Exchange.Management.Deployment.Analysis.Result result)
		{
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00025F22 File Offset: 0x00024122
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x00025F2A File Offset: 0x0002412A
		public Microsoft.Exchange.Management.Deployment.Analysis.Rule InstallWatermark { get; private set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00025F33 File Offset: 0x00024133
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x00025F3B File Offset: 0x0002413B
		public Microsoft.Exchange.Management.Deployment.Analysis.Rule LonghornIIS6MgmtConsoleNotInstalledWarning { get; private set; }

		// Token: 0x06000A4E RID: 2638 RVA: 0x00025FFC File Offset: 0x000241FC
		private void BuildPrereqRules()
		{
			PrereqAnalysisMemberBuilder build = base.Build;
			Func<Microsoft.Exchange.Management.Deployment.Analysis.AnalysisMember<string>> forEachResult = () => this.ServerRoleUnpacked;
			Optional<SetupRole> roles = SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport;
			Optional<SetupMode> modes = SetupMode.Install | SetupMode.DisasterRecovery;
			this.InstallWatermark = build.Rule<string>(forEachResult, (Microsoft.Exchange.Management.Deployment.Analysis.RuleResult x) => Strings.WatermarkPresent(x.AncestorOfType<string>(this.ServerRoleUnpacked).Value), (Microsoft.Exchange.Management.Deployment.Analysis.Result<string> x) => x.ValueOrDefault != null, default(Optional<Evaluate>), roles, modes, default(Optional<Severity>));
			PrereqAnalysisMemberBuilder build2 = base.Build;
			Optional<SetupRole> roles2 = SetupRole.Bridgehead | SetupRole.UnifiedMessaging;
			Optional<SetupMode> modes2 = SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery;
			Optional<Severity> severity = Severity.Warning;
			this.LonghornIIS6MgmtConsoleNotInstalledWarning = build2.Rule((Microsoft.Exchange.Management.Deployment.Analysis.RuleResult x) => Strings.ComponentIsRecommended("IIS 6 Management Console"), () => (this.WindowsVersion.Value == "6.0" || this.WindowsVersion.Value == "6.1") && (this.IIS6ManagementConsoleStatus.ValueOrDefault == null || this.IIS6ManagementConsoleStatus.Value == 0), default(Optional<Evaluate>), roles2, modes2, severity);
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x000260DA File Offset: 0x000242DA
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x000260E2 File Offset: 0x000242E2
		public Microsoft.Exchange.Management.Deployment.Analysis.Setting<string> Roles { get; private set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x000260EB File Offset: 0x000242EB
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x000260F3 File Offset: 0x000242F3
		public Microsoft.Exchange.Management.Deployment.Analysis.Setting<string> ServerRoleUnpacked { get; private set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x000260FC File Offset: 0x000242FC
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x00026104 File Offset: 0x00024304
		public Microsoft.Exchange.Management.Deployment.Analysis.Setting<string> Watermarks { get; private set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0002610D File Offset: 0x0002430D
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x00026115 File Offset: 0x00024315
		public Microsoft.Exchange.Management.Deployment.Analysis.Setting<string> WindowsVersion { get; private set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0002611E File Offset: 0x0002431E
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x00026126 File Offset: 0x00024326
		public Microsoft.Exchange.Management.Deployment.Analysis.Setting<int?> IIS6ManagementConsoleStatus { get; private set; }

		// Token: 0x06000A59 RID: 2649 RVA: 0x000261F8 File Offset: 0x000243F8
		private void BuildPrereqSettings()
		{
			PrereqAnalysisMemberBuilder build = base.Build;
			Optional<SetupRole> roles = SetupRole.All;
			Optional<SetupMode> modes = SetupMode.All;
			this.Roles = build.Setting<string>(() => (string[])base.DataSources.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15", null), default(Optional<Evaluate>), roles, modes);
			this.ServerRoleUnpacked = base.Build.Setting<string, string>(() => this.Roles, (Microsoft.Exchange.Management.Deployment.Analysis.Result<string> x) => AnalysisHelpers.Replace(x.Value, "(.*Role)", "$1"), default(Optional<Evaluate>), default(Optional<SetupRole>), default(Optional<SetupMode>));
			this.Watermarks = base.Build.Setting<string, string>(() => this.ServerRoleUnpacked, (Microsoft.Exchange.Management.Deployment.Analysis.Result<string> x) => (string)base.DataSources.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + x.Value, "Watermark"), default(Optional<Evaluate>), default(Optional<SetupRole>), default(Optional<SetupMode>));
			this.WindowsVersion = base.Build.Setting<string>(() => (string)base.DataSources.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\Windows NT\\CurrentVersion", "CurrentVersion"), default(Optional<Evaluate>), default(Optional<SetupRole>), default(Optional<SetupMode>));
			this.IIS6ManagementConsoleStatus = base.Build.Setting<int?>(() => (int?)base.DataSources.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "LegacySnapin"), default(Optional<Evaluate>), default(Optional<SetupRole>), default(Optional<SetupMode>));
		}

		// Token: 0x040005B4 RID: 1460
		private readonly SetupMode setupModes;

		// Token: 0x040005B5 RID: 1461
		private readonly SetupRole setupRoles;

		// Token: 0x040005B6 RID: 1462
		private readonly GlobalParameters prereqAnalysisParameters;
	}
}
