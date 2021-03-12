using System;
using System.Management.Automation;
using System.Xml;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200007F RID: 127
	public class SetAppConfigValueCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06001871 RID: 6257 RVA: 0x000375E1 File Offset: 0x000357E1
		private SetAppConfigValueCommand() : base("Set-AppConfigValue")
		{
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000375EE File Offset: 0x000357EE
		public SetAppConfigValueCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000375FD File Offset: 0x000357FD
		public virtual SetAppConfigValueCommand SetParameters(SetAppConfigValueCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00037607 File Offset: 0x00035807
		public virtual SetAppConfigValueCommand SetParameters(SetAppConfigValueCommand.AttributeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x00037611 File Offset: 0x00035811
		public virtual SetAppConfigValueCommand SetParameters(SetAppConfigValueCommand.RemoveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0003761B File Offset: 0x0003581B
		public virtual SetAppConfigValueCommand SetParameters(SetAppConfigValueCommand.AppSettingKeyParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00037625 File Offset: 0x00035825
		public virtual SetAppConfigValueCommand SetParameters(SetAppConfigValueCommand.ListValuesParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0003762F File Offset: 0x0003582F
		public virtual SetAppConfigValueCommand SetParameters(SetAppConfigValueCommand.XmlNodeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000080 RID: 128
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700032C RID: 812
			// (set) Token: 0x06001879 RID: 6265 RVA: 0x00037639 File Offset: 0x00035839
			public virtual string ConfigFileFullPath
			{
				set
				{
					base.PowerSharpParameters["ConfigFileFullPath"] = value;
				}
			}

			// Token: 0x1700032D RID: 813
			// (set) Token: 0x0600187A RID: 6266 RVA: 0x0003764C File Offset: 0x0003584C
			public virtual string Element
			{
				set
				{
					base.PowerSharpParameters["Element"] = value;
				}
			}

			// Token: 0x1700032E RID: 814
			// (set) Token: 0x0600187B RID: 6267 RVA: 0x0003765F File Offset: 0x0003585F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700032F RID: 815
			// (set) Token: 0x0600187C RID: 6268 RVA: 0x00037677 File Offset: 0x00035877
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000330 RID: 816
			// (set) Token: 0x0600187D RID: 6269 RVA: 0x0003768F File Offset: 0x0003588F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000331 RID: 817
			// (set) Token: 0x0600187E RID: 6270 RVA: 0x000376A7 File Offset: 0x000358A7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000081 RID: 129
		public class AttributeParameters : ParametersBase
		{
			// Token: 0x17000332 RID: 818
			// (set) Token: 0x06001880 RID: 6272 RVA: 0x000376C7 File Offset: 0x000358C7
			public virtual string Attribute
			{
				set
				{
					base.PowerSharpParameters["Attribute"] = value;
				}
			}

			// Token: 0x17000333 RID: 819
			// (set) Token: 0x06001881 RID: 6273 RVA: 0x000376DA File Offset: 0x000358DA
			public virtual string NewValue
			{
				set
				{
					base.PowerSharpParameters["NewValue"] = value;
				}
			}

			// Token: 0x17000334 RID: 820
			// (set) Token: 0x06001882 RID: 6274 RVA: 0x000376ED File Offset: 0x000358ED
			public virtual string OldValue
			{
				set
				{
					base.PowerSharpParameters["OldValue"] = value;
				}
			}

			// Token: 0x17000335 RID: 821
			// (set) Token: 0x06001883 RID: 6275 RVA: 0x00037700 File Offset: 0x00035900
			public virtual SwitchParameter InsertAsFirst
			{
				set
				{
					base.PowerSharpParameters["InsertAsFirst"] = value;
				}
			}

			// Token: 0x17000336 RID: 822
			// (set) Token: 0x06001884 RID: 6276 RVA: 0x00037718 File Offset: 0x00035918
			public virtual string ConfigFileFullPath
			{
				set
				{
					base.PowerSharpParameters["ConfigFileFullPath"] = value;
				}
			}

			// Token: 0x17000337 RID: 823
			// (set) Token: 0x06001885 RID: 6277 RVA: 0x0003772B File Offset: 0x0003592B
			public virtual string Element
			{
				set
				{
					base.PowerSharpParameters["Element"] = value;
				}
			}

			// Token: 0x17000338 RID: 824
			// (set) Token: 0x06001886 RID: 6278 RVA: 0x0003773E File Offset: 0x0003593E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000339 RID: 825
			// (set) Token: 0x06001887 RID: 6279 RVA: 0x00037756 File Offset: 0x00035956
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700033A RID: 826
			// (set) Token: 0x06001888 RID: 6280 RVA: 0x0003776E File Offset: 0x0003596E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700033B RID: 827
			// (set) Token: 0x06001889 RID: 6281 RVA: 0x00037786 File Offset: 0x00035986
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000082 RID: 130
		public class RemoveParameters : ParametersBase
		{
			// Token: 0x1700033C RID: 828
			// (set) Token: 0x0600188B RID: 6283 RVA: 0x000377A6 File Offset: 0x000359A6
			public virtual string AppSettingKey
			{
				set
				{
					base.PowerSharpParameters["AppSettingKey"] = value;
				}
			}

			// Token: 0x1700033D RID: 829
			// (set) Token: 0x0600188C RID: 6284 RVA: 0x000377B9 File Offset: 0x000359B9
			public virtual SwitchParameter Remove
			{
				set
				{
					base.PowerSharpParameters["Remove"] = value;
				}
			}

			// Token: 0x1700033E RID: 830
			// (set) Token: 0x0600188D RID: 6285 RVA: 0x000377D1 File Offset: 0x000359D1
			public virtual string ConfigFileFullPath
			{
				set
				{
					base.PowerSharpParameters["ConfigFileFullPath"] = value;
				}
			}

			// Token: 0x1700033F RID: 831
			// (set) Token: 0x0600188E RID: 6286 RVA: 0x000377E4 File Offset: 0x000359E4
			public virtual string Element
			{
				set
				{
					base.PowerSharpParameters["Element"] = value;
				}
			}

			// Token: 0x17000340 RID: 832
			// (set) Token: 0x0600188F RID: 6287 RVA: 0x000377F7 File Offset: 0x000359F7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000341 RID: 833
			// (set) Token: 0x06001890 RID: 6288 RVA: 0x0003780F File Offset: 0x00035A0F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000342 RID: 834
			// (set) Token: 0x06001891 RID: 6289 RVA: 0x00037827 File Offset: 0x00035A27
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000343 RID: 835
			// (set) Token: 0x06001892 RID: 6290 RVA: 0x0003783F File Offset: 0x00035A3F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000083 RID: 131
		public class AppSettingKeyParameters : ParametersBase
		{
			// Token: 0x17000344 RID: 836
			// (set) Token: 0x06001894 RID: 6292 RVA: 0x0003785F File Offset: 0x00035A5F
			public virtual string AppSettingKey
			{
				set
				{
					base.PowerSharpParameters["AppSettingKey"] = value;
				}
			}

			// Token: 0x17000345 RID: 837
			// (set) Token: 0x06001895 RID: 6293 RVA: 0x00037872 File Offset: 0x00035A72
			public virtual string NewValue
			{
				set
				{
					base.PowerSharpParameters["NewValue"] = value;
				}
			}

			// Token: 0x17000346 RID: 838
			// (set) Token: 0x06001896 RID: 6294 RVA: 0x00037885 File Offset: 0x00035A85
			public virtual string OldValue
			{
				set
				{
					base.PowerSharpParameters["OldValue"] = value;
				}
			}

			// Token: 0x17000347 RID: 839
			// (set) Token: 0x06001897 RID: 6295 RVA: 0x00037898 File Offset: 0x00035A98
			public virtual string ConfigFileFullPath
			{
				set
				{
					base.PowerSharpParameters["ConfigFileFullPath"] = value;
				}
			}

			// Token: 0x17000348 RID: 840
			// (set) Token: 0x06001898 RID: 6296 RVA: 0x000378AB File Offset: 0x00035AAB
			public virtual string Element
			{
				set
				{
					base.PowerSharpParameters["Element"] = value;
				}
			}

			// Token: 0x17000349 RID: 841
			// (set) Token: 0x06001899 RID: 6297 RVA: 0x000378BE File Offset: 0x00035ABE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700034A RID: 842
			// (set) Token: 0x0600189A RID: 6298 RVA: 0x000378D6 File Offset: 0x00035AD6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700034B RID: 843
			// (set) Token: 0x0600189B RID: 6299 RVA: 0x000378EE File Offset: 0x00035AEE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700034C RID: 844
			// (set) Token: 0x0600189C RID: 6300 RVA: 0x00037906 File Offset: 0x00035B06
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000084 RID: 132
		public class ListValuesParameters : ParametersBase
		{
			// Token: 0x1700034D RID: 845
			// (set) Token: 0x0600189E RID: 6302 RVA: 0x00037926 File Offset: 0x00035B26
			public virtual MultiValuedProperty<string> ListValues
			{
				set
				{
					base.PowerSharpParameters["ListValues"] = value;
				}
			}

			// Token: 0x1700034E RID: 846
			// (set) Token: 0x0600189F RID: 6303 RVA: 0x00037939 File Offset: 0x00035B39
			public virtual string ConfigFileFullPath
			{
				set
				{
					base.PowerSharpParameters["ConfigFileFullPath"] = value;
				}
			}

			// Token: 0x1700034F RID: 847
			// (set) Token: 0x060018A0 RID: 6304 RVA: 0x0003794C File Offset: 0x00035B4C
			public virtual string Element
			{
				set
				{
					base.PowerSharpParameters["Element"] = value;
				}
			}

			// Token: 0x17000350 RID: 848
			// (set) Token: 0x060018A1 RID: 6305 RVA: 0x0003795F File Offset: 0x00035B5F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000351 RID: 849
			// (set) Token: 0x060018A2 RID: 6306 RVA: 0x00037977 File Offset: 0x00035B77
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000352 RID: 850
			// (set) Token: 0x060018A3 RID: 6307 RVA: 0x0003798F File Offset: 0x00035B8F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000353 RID: 851
			// (set) Token: 0x060018A4 RID: 6308 RVA: 0x000379A7 File Offset: 0x00035BA7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000085 RID: 133
		public class XmlNodeParameters : ParametersBase
		{
			// Token: 0x17000354 RID: 852
			// (set) Token: 0x060018A6 RID: 6310 RVA: 0x000379C7 File Offset: 0x00035BC7
			public virtual SwitchParameter InsertAsFirst
			{
				set
				{
					base.PowerSharpParameters["InsertAsFirst"] = value;
				}
			}

			// Token: 0x17000355 RID: 853
			// (set) Token: 0x060018A7 RID: 6311 RVA: 0x000379DF File Offset: 0x00035BDF
			public virtual XmlNode XmlNode
			{
				set
				{
					base.PowerSharpParameters["XmlNode"] = value;
				}
			}

			// Token: 0x17000356 RID: 854
			// (set) Token: 0x060018A8 RID: 6312 RVA: 0x000379F2 File Offset: 0x00035BF2
			public virtual string ConfigFileFullPath
			{
				set
				{
					base.PowerSharpParameters["ConfigFileFullPath"] = value;
				}
			}

			// Token: 0x17000357 RID: 855
			// (set) Token: 0x060018A9 RID: 6313 RVA: 0x00037A05 File Offset: 0x00035C05
			public virtual string Element
			{
				set
				{
					base.PowerSharpParameters["Element"] = value;
				}
			}

			// Token: 0x17000358 RID: 856
			// (set) Token: 0x060018AA RID: 6314 RVA: 0x00037A18 File Offset: 0x00035C18
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000359 RID: 857
			// (set) Token: 0x060018AB RID: 6315 RVA: 0x00037A30 File Offset: 0x00035C30
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700035A RID: 858
			// (set) Token: 0x060018AC RID: 6316 RVA: 0x00037A48 File Offset: 0x00035C48
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700035B RID: 859
			// (set) Token: 0x060018AD RID: 6317 RVA: 0x00037A60 File Offset: 0x00035C60
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
