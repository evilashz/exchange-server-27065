using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B35 RID: 2869
	public class ExportUMPromptCommand : SyntheticCommandWithPipelineInput<UMDialPlanIdParameter, UMDialPlanIdParameter>
	{
		// Token: 0x06008C1B RID: 35867 RVA: 0x000CD9BF File Offset: 0x000CBBBF
		private ExportUMPromptCommand() : base("Export-UMPrompt")
		{
		}

		// Token: 0x06008C1C RID: 35868 RVA: 0x000CD9CC File Offset: 0x000CBBCC
		public ExportUMPromptCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008C1D RID: 35869 RVA: 0x000CD9DB File Offset: 0x000CBBDB
		public virtual ExportUMPromptCommand SetParameters(ExportUMPromptCommand.AfterHoursWelcomeGreetingAndMenuParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008C1E RID: 35870 RVA: 0x000CD9E5 File Offset: 0x000CBBE5
		public virtual ExportUMPromptCommand SetParameters(ExportUMPromptCommand.AfterHoursWelcomeGreetingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008C1F RID: 35871 RVA: 0x000CD9EF File Offset: 0x000CBBEF
		public virtual ExportUMPromptCommand SetParameters(ExportUMPromptCommand.BusinessHoursParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008C20 RID: 35872 RVA: 0x000CD9F9 File Offset: 0x000CBBF9
		public virtual ExportUMPromptCommand SetParameters(ExportUMPromptCommand.BusinessLocationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008C21 RID: 35873 RVA: 0x000CDA03 File Offset: 0x000CBC03
		public virtual ExportUMPromptCommand SetParameters(ExportUMPromptCommand.BusinessHoursWelcomeGreetingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008C22 RID: 35874 RVA: 0x000CDA0D File Offset: 0x000CBC0D
		public virtual ExportUMPromptCommand SetParameters(ExportUMPromptCommand.BusinessHoursWelcomeGreetingAndMenuParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008C23 RID: 35875 RVA: 0x000CDA17 File Offset: 0x000CBC17
		public virtual ExportUMPromptCommand SetParameters(ExportUMPromptCommand.AACustomGreetingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008C24 RID: 35876 RVA: 0x000CDA21 File Offset: 0x000CBC21
		public virtual ExportUMPromptCommand SetParameters(ExportUMPromptCommand.DPCustomGreetingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008C25 RID: 35877 RVA: 0x000CDA2B File Offset: 0x000CBC2B
		public virtual ExportUMPromptCommand SetParameters(ExportUMPromptCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B36 RID: 2870
		public class AfterHoursWelcomeGreetingAndMenuParameters : ParametersBase
		{
			// Token: 0x1700616A RID: 24938
			// (set) Token: 0x06008C26 RID: 35878 RVA: 0x000CDA35 File Offset: 0x000CBC35
			public virtual string UMAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["UMAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x1700616B RID: 24939
			// (set) Token: 0x06008C27 RID: 35879 RVA: 0x000CDA53 File Offset: 0x000CBC53
			public virtual SwitchParameter AfterHoursWelcomeGreetingAndMenu
			{
				set
				{
					base.PowerSharpParameters["AfterHoursWelcomeGreetingAndMenu"] = value;
				}
			}

			// Token: 0x1700616C RID: 24940
			// (set) Token: 0x06008C28 RID: 35880 RVA: 0x000CDA6B File Offset: 0x000CBC6B
			public virtual CustomMenuKeyMapping TestMenuKeyMapping
			{
				set
				{
					base.PowerSharpParameters["TestMenuKeyMapping"] = value;
				}
			}

			// Token: 0x1700616D RID: 24941
			// (set) Token: 0x06008C29 RID: 35881 RVA: 0x000CDA7E File Offset: 0x000CBC7E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700616E RID: 24942
			// (set) Token: 0x06008C2A RID: 35882 RVA: 0x000CDA91 File Offset: 0x000CBC91
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700616F RID: 24943
			// (set) Token: 0x06008C2B RID: 35883 RVA: 0x000CDAA9 File Offset: 0x000CBCA9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006170 RID: 24944
			// (set) Token: 0x06008C2C RID: 35884 RVA: 0x000CDAC1 File Offset: 0x000CBCC1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006171 RID: 24945
			// (set) Token: 0x06008C2D RID: 35885 RVA: 0x000CDAD9 File Offset: 0x000CBCD9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006172 RID: 24946
			// (set) Token: 0x06008C2E RID: 35886 RVA: 0x000CDAF1 File Offset: 0x000CBCF1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B37 RID: 2871
		public class AfterHoursWelcomeGreetingParameters : ParametersBase
		{
			// Token: 0x17006173 RID: 24947
			// (set) Token: 0x06008C30 RID: 35888 RVA: 0x000CDB11 File Offset: 0x000CBD11
			public virtual string UMAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["UMAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x17006174 RID: 24948
			// (set) Token: 0x06008C31 RID: 35889 RVA: 0x000CDB2F File Offset: 0x000CBD2F
			public virtual SwitchParameter AfterHoursWelcomeGreeting
			{
				set
				{
					base.PowerSharpParameters["AfterHoursWelcomeGreeting"] = value;
				}
			}

			// Token: 0x17006175 RID: 24949
			// (set) Token: 0x06008C32 RID: 35890 RVA: 0x000CDB47 File Offset: 0x000CBD47
			public virtual string TestBusinessName
			{
				set
				{
					base.PowerSharpParameters["TestBusinessName"] = value;
				}
			}

			// Token: 0x17006176 RID: 24950
			// (set) Token: 0x06008C33 RID: 35891 RVA: 0x000CDB5A File Offset: 0x000CBD5A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006177 RID: 24951
			// (set) Token: 0x06008C34 RID: 35892 RVA: 0x000CDB6D File Offset: 0x000CBD6D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006178 RID: 24952
			// (set) Token: 0x06008C35 RID: 35893 RVA: 0x000CDB85 File Offset: 0x000CBD85
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006179 RID: 24953
			// (set) Token: 0x06008C36 RID: 35894 RVA: 0x000CDB9D File Offset: 0x000CBD9D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700617A RID: 24954
			// (set) Token: 0x06008C37 RID: 35895 RVA: 0x000CDBB5 File Offset: 0x000CBDB5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700617B RID: 24955
			// (set) Token: 0x06008C38 RID: 35896 RVA: 0x000CDBCD File Offset: 0x000CBDCD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B38 RID: 2872
		public class BusinessHoursParameters : ParametersBase
		{
			// Token: 0x1700617C RID: 24956
			// (set) Token: 0x06008C3A RID: 35898 RVA: 0x000CDBED File Offset: 0x000CBDED
			public virtual string UMAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["UMAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x1700617D RID: 24957
			// (set) Token: 0x06008C3B RID: 35899 RVA: 0x000CDC0B File Offset: 0x000CBE0B
			public virtual SwitchParameter BusinessHours
			{
				set
				{
					base.PowerSharpParameters["BusinessHours"] = value;
				}
			}

			// Token: 0x1700617E RID: 24958
			// (set) Token: 0x06008C3C RID: 35900 RVA: 0x000CDC23 File Offset: 0x000CBE23
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700617F RID: 24959
			// (set) Token: 0x06008C3D RID: 35901 RVA: 0x000CDC36 File Offset: 0x000CBE36
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006180 RID: 24960
			// (set) Token: 0x06008C3E RID: 35902 RVA: 0x000CDC4E File Offset: 0x000CBE4E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006181 RID: 24961
			// (set) Token: 0x06008C3F RID: 35903 RVA: 0x000CDC66 File Offset: 0x000CBE66
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006182 RID: 24962
			// (set) Token: 0x06008C40 RID: 35904 RVA: 0x000CDC7E File Offset: 0x000CBE7E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006183 RID: 24963
			// (set) Token: 0x06008C41 RID: 35905 RVA: 0x000CDC96 File Offset: 0x000CBE96
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B39 RID: 2873
		public class BusinessLocationParameters : ParametersBase
		{
			// Token: 0x17006184 RID: 24964
			// (set) Token: 0x06008C43 RID: 35907 RVA: 0x000CDCB6 File Offset: 0x000CBEB6
			public virtual string UMAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["UMAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x17006185 RID: 24965
			// (set) Token: 0x06008C44 RID: 35908 RVA: 0x000CDCD4 File Offset: 0x000CBED4
			public virtual SwitchParameter BusinessLocation
			{
				set
				{
					base.PowerSharpParameters["BusinessLocation"] = value;
				}
			}

			// Token: 0x17006186 RID: 24966
			// (set) Token: 0x06008C45 RID: 35909 RVA: 0x000CDCEC File Offset: 0x000CBEEC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006187 RID: 24967
			// (set) Token: 0x06008C46 RID: 35910 RVA: 0x000CDCFF File Offset: 0x000CBEFF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006188 RID: 24968
			// (set) Token: 0x06008C47 RID: 35911 RVA: 0x000CDD17 File Offset: 0x000CBF17
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006189 RID: 24969
			// (set) Token: 0x06008C48 RID: 35912 RVA: 0x000CDD2F File Offset: 0x000CBF2F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700618A RID: 24970
			// (set) Token: 0x06008C49 RID: 35913 RVA: 0x000CDD47 File Offset: 0x000CBF47
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700618B RID: 24971
			// (set) Token: 0x06008C4A RID: 35914 RVA: 0x000CDD5F File Offset: 0x000CBF5F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B3A RID: 2874
		public class BusinessHoursWelcomeGreetingParameters : ParametersBase
		{
			// Token: 0x1700618C RID: 24972
			// (set) Token: 0x06008C4C RID: 35916 RVA: 0x000CDD7F File Offset: 0x000CBF7F
			public virtual string UMAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["UMAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x1700618D RID: 24973
			// (set) Token: 0x06008C4D RID: 35917 RVA: 0x000CDD9D File Offset: 0x000CBF9D
			public virtual SwitchParameter BusinessHoursWelcomeGreeting
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursWelcomeGreeting"] = value;
				}
			}

			// Token: 0x1700618E RID: 24974
			// (set) Token: 0x06008C4E RID: 35918 RVA: 0x000CDDB5 File Offset: 0x000CBFB5
			public virtual string TestBusinessName
			{
				set
				{
					base.PowerSharpParameters["TestBusinessName"] = value;
				}
			}

			// Token: 0x1700618F RID: 24975
			// (set) Token: 0x06008C4F RID: 35919 RVA: 0x000CDDC8 File Offset: 0x000CBFC8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006190 RID: 24976
			// (set) Token: 0x06008C50 RID: 35920 RVA: 0x000CDDDB File Offset: 0x000CBFDB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006191 RID: 24977
			// (set) Token: 0x06008C51 RID: 35921 RVA: 0x000CDDF3 File Offset: 0x000CBFF3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006192 RID: 24978
			// (set) Token: 0x06008C52 RID: 35922 RVA: 0x000CDE0B File Offset: 0x000CC00B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006193 RID: 24979
			// (set) Token: 0x06008C53 RID: 35923 RVA: 0x000CDE23 File Offset: 0x000CC023
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006194 RID: 24980
			// (set) Token: 0x06008C54 RID: 35924 RVA: 0x000CDE3B File Offset: 0x000CC03B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B3B RID: 2875
		public class BusinessHoursWelcomeGreetingAndMenuParameters : ParametersBase
		{
			// Token: 0x17006195 RID: 24981
			// (set) Token: 0x06008C56 RID: 35926 RVA: 0x000CDE5B File Offset: 0x000CC05B
			public virtual string UMAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["UMAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x17006196 RID: 24982
			// (set) Token: 0x06008C57 RID: 35927 RVA: 0x000CDE79 File Offset: 0x000CC079
			public virtual SwitchParameter BusinessHoursWelcomeGreetingAndMenu
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursWelcomeGreetingAndMenu"] = value;
				}
			}

			// Token: 0x17006197 RID: 24983
			// (set) Token: 0x06008C58 RID: 35928 RVA: 0x000CDE91 File Offset: 0x000CC091
			public virtual CustomMenuKeyMapping TestMenuKeyMapping
			{
				set
				{
					base.PowerSharpParameters["TestMenuKeyMapping"] = value;
				}
			}

			// Token: 0x17006198 RID: 24984
			// (set) Token: 0x06008C59 RID: 35929 RVA: 0x000CDEA4 File Offset: 0x000CC0A4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006199 RID: 24985
			// (set) Token: 0x06008C5A RID: 35930 RVA: 0x000CDEB7 File Offset: 0x000CC0B7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700619A RID: 24986
			// (set) Token: 0x06008C5B RID: 35931 RVA: 0x000CDECF File Offset: 0x000CC0CF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700619B RID: 24987
			// (set) Token: 0x06008C5C RID: 35932 RVA: 0x000CDEE7 File Offset: 0x000CC0E7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700619C RID: 24988
			// (set) Token: 0x06008C5D RID: 35933 RVA: 0x000CDEFF File Offset: 0x000CC0FF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700619D RID: 24989
			// (set) Token: 0x06008C5E RID: 35934 RVA: 0x000CDF17 File Offset: 0x000CC117
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B3C RID: 2876
		public class AACustomGreetingParameters : ParametersBase
		{
			// Token: 0x1700619E RID: 24990
			// (set) Token: 0x06008C60 RID: 35936 RVA: 0x000CDF37 File Offset: 0x000CC137
			public virtual string UMAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["UMAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x1700619F RID: 24991
			// (set) Token: 0x06008C61 RID: 35937 RVA: 0x000CDF55 File Offset: 0x000CC155
			public virtual string PromptFileName
			{
				set
				{
					base.PowerSharpParameters["PromptFileName"] = value;
				}
			}

			// Token: 0x170061A0 RID: 24992
			// (set) Token: 0x06008C62 RID: 35938 RVA: 0x000CDF68 File Offset: 0x000CC168
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061A1 RID: 24993
			// (set) Token: 0x06008C63 RID: 35939 RVA: 0x000CDF7B File Offset: 0x000CC17B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061A2 RID: 24994
			// (set) Token: 0x06008C64 RID: 35940 RVA: 0x000CDF93 File Offset: 0x000CC193
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061A3 RID: 24995
			// (set) Token: 0x06008C65 RID: 35941 RVA: 0x000CDFAB File Offset: 0x000CC1AB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061A4 RID: 24996
			// (set) Token: 0x06008C66 RID: 35942 RVA: 0x000CDFC3 File Offset: 0x000CC1C3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170061A5 RID: 24997
			// (set) Token: 0x06008C67 RID: 35943 RVA: 0x000CDFDB File Offset: 0x000CC1DB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B3D RID: 2877
		public class DPCustomGreetingParameters : ParametersBase
		{
			// Token: 0x170061A6 RID: 24998
			// (set) Token: 0x06008C69 RID: 35945 RVA: 0x000CDFFB File Offset: 0x000CC1FB
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170061A7 RID: 24999
			// (set) Token: 0x06008C6A RID: 35946 RVA: 0x000CE019 File Offset: 0x000CC219
			public virtual string PromptFileName
			{
				set
				{
					base.PowerSharpParameters["PromptFileName"] = value;
				}
			}

			// Token: 0x170061A8 RID: 25000
			// (set) Token: 0x06008C6B RID: 35947 RVA: 0x000CE02C File Offset: 0x000CC22C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061A9 RID: 25001
			// (set) Token: 0x06008C6C RID: 35948 RVA: 0x000CE03F File Offset: 0x000CC23F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061AA RID: 25002
			// (set) Token: 0x06008C6D RID: 35949 RVA: 0x000CE057 File Offset: 0x000CC257
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061AB RID: 25003
			// (set) Token: 0x06008C6E RID: 35950 RVA: 0x000CE06F File Offset: 0x000CC26F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061AC RID: 25004
			// (set) Token: 0x06008C6F RID: 35951 RVA: 0x000CE087 File Offset: 0x000CC287
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170061AD RID: 25005
			// (set) Token: 0x06008C70 RID: 35952 RVA: 0x000CE09F File Offset: 0x000CC29F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B3E RID: 2878
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170061AE RID: 25006
			// (set) Token: 0x06008C72 RID: 35954 RVA: 0x000CE0BF File Offset: 0x000CC2BF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061AF RID: 25007
			// (set) Token: 0x06008C73 RID: 35955 RVA: 0x000CE0D2 File Offset: 0x000CC2D2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061B0 RID: 25008
			// (set) Token: 0x06008C74 RID: 35956 RVA: 0x000CE0EA File Offset: 0x000CC2EA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061B1 RID: 25009
			// (set) Token: 0x06008C75 RID: 35957 RVA: 0x000CE102 File Offset: 0x000CC302
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061B2 RID: 25010
			// (set) Token: 0x06008C76 RID: 35958 RVA: 0x000CE11A File Offset: 0x000CC31A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170061B3 RID: 25011
			// (set) Token: 0x06008C77 RID: 35959 RVA: 0x000CE132 File Offset: 0x000CC332
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
