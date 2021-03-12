using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000449 RID: 1097
	public class SetCalendarProcessingCommand : SyntheticCommandWithPipelineInputNoOutput<CalendarConfiguration>
	{
		// Token: 0x06003F4E RID: 16206 RVA: 0x00069E76 File Offset: 0x00068076
		private SetCalendarProcessingCommand() : base("Set-CalendarProcessing")
		{
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x00069E83 File Offset: 0x00068083
		public SetCalendarProcessingCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x00069E92 File Offset: 0x00068092
		public virtual SetCalendarProcessingCommand SetParameters(SetCalendarProcessingCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x00069E9C File Offset: 0x0006809C
		public virtual SetCalendarProcessingCommand SetParameters(SetCalendarProcessingCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200044A RID: 1098
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002275 RID: 8821
			// (set) Token: 0x06003F52 RID: 16210 RVA: 0x00069EA6 File Offset: 0x000680A6
			public virtual RecipientIdParameter ResourceDelegates
			{
				set
				{
					base.PowerSharpParameters["ResourceDelegates"] = value;
				}
			}

			// Token: 0x17002276 RID: 8822
			// (set) Token: 0x06003F53 RID: 16211 RVA: 0x00069EB9 File Offset: 0x000680B9
			public virtual RecipientIdParameter RequestOutOfPolicy
			{
				set
				{
					base.PowerSharpParameters["RequestOutOfPolicy"] = value;
				}
			}

			// Token: 0x17002277 RID: 8823
			// (set) Token: 0x06003F54 RID: 16212 RVA: 0x00069ECC File Offset: 0x000680CC
			public virtual RecipientIdParameter BookInPolicy
			{
				set
				{
					base.PowerSharpParameters["BookInPolicy"] = value;
				}
			}

			// Token: 0x17002278 RID: 8824
			// (set) Token: 0x06003F55 RID: 16213 RVA: 0x00069EDF File Offset: 0x000680DF
			public virtual RecipientIdParameter RequestInPolicy
			{
				set
				{
					base.PowerSharpParameters["RequestInPolicy"] = value;
				}
			}

			// Token: 0x17002279 RID: 8825
			// (set) Token: 0x06003F56 RID: 16214 RVA: 0x00069EF2 File Offset: 0x000680F2
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700227A RID: 8826
			// (set) Token: 0x06003F57 RID: 16215 RVA: 0x00069F0A File Offset: 0x0006810A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700227B RID: 8827
			// (set) Token: 0x06003F58 RID: 16216 RVA: 0x00069F1D File Offset: 0x0006811D
			public virtual CalendarProcessingFlags AutomateProcessing
			{
				set
				{
					base.PowerSharpParameters["AutomateProcessing"] = value;
				}
			}

			// Token: 0x1700227C RID: 8828
			// (set) Token: 0x06003F59 RID: 16217 RVA: 0x00069F35 File Offset: 0x00068135
			public virtual bool AllowConflicts
			{
				set
				{
					base.PowerSharpParameters["AllowConflicts"] = value;
				}
			}

			// Token: 0x1700227D RID: 8829
			// (set) Token: 0x06003F5A RID: 16218 RVA: 0x00069F4D File Offset: 0x0006814D
			public virtual int BookingWindowInDays
			{
				set
				{
					base.PowerSharpParameters["BookingWindowInDays"] = value;
				}
			}

			// Token: 0x1700227E RID: 8830
			// (set) Token: 0x06003F5B RID: 16219 RVA: 0x00069F65 File Offset: 0x00068165
			public virtual int MaximumDurationInMinutes
			{
				set
				{
					base.PowerSharpParameters["MaximumDurationInMinutes"] = value;
				}
			}

			// Token: 0x1700227F RID: 8831
			// (set) Token: 0x06003F5C RID: 16220 RVA: 0x00069F7D File Offset: 0x0006817D
			public virtual bool AllowRecurringMeetings
			{
				set
				{
					base.PowerSharpParameters["AllowRecurringMeetings"] = value;
				}
			}

			// Token: 0x17002280 RID: 8832
			// (set) Token: 0x06003F5D RID: 16221 RVA: 0x00069F95 File Offset: 0x00068195
			public virtual bool EnforceSchedulingHorizon
			{
				set
				{
					base.PowerSharpParameters["EnforceSchedulingHorizon"] = value;
				}
			}

			// Token: 0x17002281 RID: 8833
			// (set) Token: 0x06003F5E RID: 16222 RVA: 0x00069FAD File Offset: 0x000681AD
			public virtual bool ScheduleOnlyDuringWorkHours
			{
				set
				{
					base.PowerSharpParameters["ScheduleOnlyDuringWorkHours"] = value;
				}
			}

			// Token: 0x17002282 RID: 8834
			// (set) Token: 0x06003F5F RID: 16223 RVA: 0x00069FC5 File Offset: 0x000681C5
			public virtual int ConflictPercentageAllowed
			{
				set
				{
					base.PowerSharpParameters["ConflictPercentageAllowed"] = value;
				}
			}

			// Token: 0x17002283 RID: 8835
			// (set) Token: 0x06003F60 RID: 16224 RVA: 0x00069FDD File Offset: 0x000681DD
			public virtual int MaximumConflictInstances
			{
				set
				{
					base.PowerSharpParameters["MaximumConflictInstances"] = value;
				}
			}

			// Token: 0x17002284 RID: 8836
			// (set) Token: 0x06003F61 RID: 16225 RVA: 0x00069FF5 File Offset: 0x000681F5
			public virtual bool ForwardRequestsToDelegates
			{
				set
				{
					base.PowerSharpParameters["ForwardRequestsToDelegates"] = value;
				}
			}

			// Token: 0x17002285 RID: 8837
			// (set) Token: 0x06003F62 RID: 16226 RVA: 0x0006A00D File Offset: 0x0006820D
			public virtual bool DeleteAttachments
			{
				set
				{
					base.PowerSharpParameters["DeleteAttachments"] = value;
				}
			}

			// Token: 0x17002286 RID: 8838
			// (set) Token: 0x06003F63 RID: 16227 RVA: 0x0006A025 File Offset: 0x00068225
			public virtual bool DeleteComments
			{
				set
				{
					base.PowerSharpParameters["DeleteComments"] = value;
				}
			}

			// Token: 0x17002287 RID: 8839
			// (set) Token: 0x06003F64 RID: 16228 RVA: 0x0006A03D File Offset: 0x0006823D
			public virtual bool RemovePrivateProperty
			{
				set
				{
					base.PowerSharpParameters["RemovePrivateProperty"] = value;
				}
			}

			// Token: 0x17002288 RID: 8840
			// (set) Token: 0x06003F65 RID: 16229 RVA: 0x0006A055 File Offset: 0x00068255
			public virtual bool DeleteSubject
			{
				set
				{
					base.PowerSharpParameters["DeleteSubject"] = value;
				}
			}

			// Token: 0x17002289 RID: 8841
			// (set) Token: 0x06003F66 RID: 16230 RVA: 0x0006A06D File Offset: 0x0006826D
			public virtual bool AddOrganizerToSubject
			{
				set
				{
					base.PowerSharpParameters["AddOrganizerToSubject"] = value;
				}
			}

			// Token: 0x1700228A RID: 8842
			// (set) Token: 0x06003F67 RID: 16231 RVA: 0x0006A085 File Offset: 0x00068285
			public virtual bool DeleteNonCalendarItems
			{
				set
				{
					base.PowerSharpParameters["DeleteNonCalendarItems"] = value;
				}
			}

			// Token: 0x1700228B RID: 8843
			// (set) Token: 0x06003F68 RID: 16232 RVA: 0x0006A09D File Offset: 0x0006829D
			public virtual bool TentativePendingApproval
			{
				set
				{
					base.PowerSharpParameters["TentativePendingApproval"] = value;
				}
			}

			// Token: 0x1700228C RID: 8844
			// (set) Token: 0x06003F69 RID: 16233 RVA: 0x0006A0B5 File Offset: 0x000682B5
			public virtual bool EnableResponseDetails
			{
				set
				{
					base.PowerSharpParameters["EnableResponseDetails"] = value;
				}
			}

			// Token: 0x1700228D RID: 8845
			// (set) Token: 0x06003F6A RID: 16234 RVA: 0x0006A0CD File Offset: 0x000682CD
			public virtual bool OrganizerInfo
			{
				set
				{
					base.PowerSharpParameters["OrganizerInfo"] = value;
				}
			}

			// Token: 0x1700228E RID: 8846
			// (set) Token: 0x06003F6B RID: 16235 RVA: 0x0006A0E5 File Offset: 0x000682E5
			public virtual bool AllRequestOutOfPolicy
			{
				set
				{
					base.PowerSharpParameters["AllRequestOutOfPolicy"] = value;
				}
			}

			// Token: 0x1700228F RID: 8847
			// (set) Token: 0x06003F6C RID: 16236 RVA: 0x0006A0FD File Offset: 0x000682FD
			public virtual bool AllBookInPolicy
			{
				set
				{
					base.PowerSharpParameters["AllBookInPolicy"] = value;
				}
			}

			// Token: 0x17002290 RID: 8848
			// (set) Token: 0x06003F6D RID: 16237 RVA: 0x0006A115 File Offset: 0x00068315
			public virtual bool AllRequestInPolicy
			{
				set
				{
					base.PowerSharpParameters["AllRequestInPolicy"] = value;
				}
			}

			// Token: 0x17002291 RID: 8849
			// (set) Token: 0x06003F6E RID: 16238 RVA: 0x0006A12D File Offset: 0x0006832D
			public virtual bool AddAdditionalResponse
			{
				set
				{
					base.PowerSharpParameters["AddAdditionalResponse"] = value;
				}
			}

			// Token: 0x17002292 RID: 8850
			// (set) Token: 0x06003F6F RID: 16239 RVA: 0x0006A145 File Offset: 0x00068345
			public virtual string AdditionalResponse
			{
				set
				{
					base.PowerSharpParameters["AdditionalResponse"] = value;
				}
			}

			// Token: 0x17002293 RID: 8851
			// (set) Token: 0x06003F70 RID: 16240 RVA: 0x0006A158 File Offset: 0x00068358
			public virtual bool RemoveOldMeetingMessages
			{
				set
				{
					base.PowerSharpParameters["RemoveOldMeetingMessages"] = value;
				}
			}

			// Token: 0x17002294 RID: 8852
			// (set) Token: 0x06003F71 RID: 16241 RVA: 0x0006A170 File Offset: 0x00068370
			public virtual bool AddNewRequestsTentatively
			{
				set
				{
					base.PowerSharpParameters["AddNewRequestsTentatively"] = value;
				}
			}

			// Token: 0x17002295 RID: 8853
			// (set) Token: 0x06003F72 RID: 16242 RVA: 0x0006A188 File Offset: 0x00068388
			public virtual bool ProcessExternalMeetingMessages
			{
				set
				{
					base.PowerSharpParameters["ProcessExternalMeetingMessages"] = value;
				}
			}

			// Token: 0x17002296 RID: 8854
			// (set) Token: 0x06003F73 RID: 16243 RVA: 0x0006A1A0 File Offset: 0x000683A0
			public virtual bool RemoveForwardedMeetingNotifications
			{
				set
				{
					base.PowerSharpParameters["RemoveForwardedMeetingNotifications"] = value;
				}
			}

			// Token: 0x17002297 RID: 8855
			// (set) Token: 0x06003F74 RID: 16244 RVA: 0x0006A1B8 File Offset: 0x000683B8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002298 RID: 8856
			// (set) Token: 0x06003F75 RID: 16245 RVA: 0x0006A1D0 File Offset: 0x000683D0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002299 RID: 8857
			// (set) Token: 0x06003F76 RID: 16246 RVA: 0x0006A1E8 File Offset: 0x000683E8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700229A RID: 8858
			// (set) Token: 0x06003F77 RID: 16247 RVA: 0x0006A200 File Offset: 0x00068400
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700229B RID: 8859
			// (set) Token: 0x06003F78 RID: 16248 RVA: 0x0006A218 File Offset: 0x00068418
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200044B RID: 1099
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700229C RID: 8860
			// (set) Token: 0x06003F7A RID: 16250 RVA: 0x0006A238 File Offset: 0x00068438
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700229D RID: 8861
			// (set) Token: 0x06003F7B RID: 16251 RVA: 0x0006A256 File Offset: 0x00068456
			public virtual RecipientIdParameter ResourceDelegates
			{
				set
				{
					base.PowerSharpParameters["ResourceDelegates"] = value;
				}
			}

			// Token: 0x1700229E RID: 8862
			// (set) Token: 0x06003F7C RID: 16252 RVA: 0x0006A269 File Offset: 0x00068469
			public virtual RecipientIdParameter RequestOutOfPolicy
			{
				set
				{
					base.PowerSharpParameters["RequestOutOfPolicy"] = value;
				}
			}

			// Token: 0x1700229F RID: 8863
			// (set) Token: 0x06003F7D RID: 16253 RVA: 0x0006A27C File Offset: 0x0006847C
			public virtual RecipientIdParameter BookInPolicy
			{
				set
				{
					base.PowerSharpParameters["BookInPolicy"] = value;
				}
			}

			// Token: 0x170022A0 RID: 8864
			// (set) Token: 0x06003F7E RID: 16254 RVA: 0x0006A28F File Offset: 0x0006848F
			public virtual RecipientIdParameter RequestInPolicy
			{
				set
				{
					base.PowerSharpParameters["RequestInPolicy"] = value;
				}
			}

			// Token: 0x170022A1 RID: 8865
			// (set) Token: 0x06003F7F RID: 16255 RVA: 0x0006A2A2 File Offset: 0x000684A2
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170022A2 RID: 8866
			// (set) Token: 0x06003F80 RID: 16256 RVA: 0x0006A2BA File Offset: 0x000684BA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170022A3 RID: 8867
			// (set) Token: 0x06003F81 RID: 16257 RVA: 0x0006A2CD File Offset: 0x000684CD
			public virtual CalendarProcessingFlags AutomateProcessing
			{
				set
				{
					base.PowerSharpParameters["AutomateProcessing"] = value;
				}
			}

			// Token: 0x170022A4 RID: 8868
			// (set) Token: 0x06003F82 RID: 16258 RVA: 0x0006A2E5 File Offset: 0x000684E5
			public virtual bool AllowConflicts
			{
				set
				{
					base.PowerSharpParameters["AllowConflicts"] = value;
				}
			}

			// Token: 0x170022A5 RID: 8869
			// (set) Token: 0x06003F83 RID: 16259 RVA: 0x0006A2FD File Offset: 0x000684FD
			public virtual int BookingWindowInDays
			{
				set
				{
					base.PowerSharpParameters["BookingWindowInDays"] = value;
				}
			}

			// Token: 0x170022A6 RID: 8870
			// (set) Token: 0x06003F84 RID: 16260 RVA: 0x0006A315 File Offset: 0x00068515
			public virtual int MaximumDurationInMinutes
			{
				set
				{
					base.PowerSharpParameters["MaximumDurationInMinutes"] = value;
				}
			}

			// Token: 0x170022A7 RID: 8871
			// (set) Token: 0x06003F85 RID: 16261 RVA: 0x0006A32D File Offset: 0x0006852D
			public virtual bool AllowRecurringMeetings
			{
				set
				{
					base.PowerSharpParameters["AllowRecurringMeetings"] = value;
				}
			}

			// Token: 0x170022A8 RID: 8872
			// (set) Token: 0x06003F86 RID: 16262 RVA: 0x0006A345 File Offset: 0x00068545
			public virtual bool EnforceSchedulingHorizon
			{
				set
				{
					base.PowerSharpParameters["EnforceSchedulingHorizon"] = value;
				}
			}

			// Token: 0x170022A9 RID: 8873
			// (set) Token: 0x06003F87 RID: 16263 RVA: 0x0006A35D File Offset: 0x0006855D
			public virtual bool ScheduleOnlyDuringWorkHours
			{
				set
				{
					base.PowerSharpParameters["ScheduleOnlyDuringWorkHours"] = value;
				}
			}

			// Token: 0x170022AA RID: 8874
			// (set) Token: 0x06003F88 RID: 16264 RVA: 0x0006A375 File Offset: 0x00068575
			public virtual int ConflictPercentageAllowed
			{
				set
				{
					base.PowerSharpParameters["ConflictPercentageAllowed"] = value;
				}
			}

			// Token: 0x170022AB RID: 8875
			// (set) Token: 0x06003F89 RID: 16265 RVA: 0x0006A38D File Offset: 0x0006858D
			public virtual int MaximumConflictInstances
			{
				set
				{
					base.PowerSharpParameters["MaximumConflictInstances"] = value;
				}
			}

			// Token: 0x170022AC RID: 8876
			// (set) Token: 0x06003F8A RID: 16266 RVA: 0x0006A3A5 File Offset: 0x000685A5
			public virtual bool ForwardRequestsToDelegates
			{
				set
				{
					base.PowerSharpParameters["ForwardRequestsToDelegates"] = value;
				}
			}

			// Token: 0x170022AD RID: 8877
			// (set) Token: 0x06003F8B RID: 16267 RVA: 0x0006A3BD File Offset: 0x000685BD
			public virtual bool DeleteAttachments
			{
				set
				{
					base.PowerSharpParameters["DeleteAttachments"] = value;
				}
			}

			// Token: 0x170022AE RID: 8878
			// (set) Token: 0x06003F8C RID: 16268 RVA: 0x0006A3D5 File Offset: 0x000685D5
			public virtual bool DeleteComments
			{
				set
				{
					base.PowerSharpParameters["DeleteComments"] = value;
				}
			}

			// Token: 0x170022AF RID: 8879
			// (set) Token: 0x06003F8D RID: 16269 RVA: 0x0006A3ED File Offset: 0x000685ED
			public virtual bool RemovePrivateProperty
			{
				set
				{
					base.PowerSharpParameters["RemovePrivateProperty"] = value;
				}
			}

			// Token: 0x170022B0 RID: 8880
			// (set) Token: 0x06003F8E RID: 16270 RVA: 0x0006A405 File Offset: 0x00068605
			public virtual bool DeleteSubject
			{
				set
				{
					base.PowerSharpParameters["DeleteSubject"] = value;
				}
			}

			// Token: 0x170022B1 RID: 8881
			// (set) Token: 0x06003F8F RID: 16271 RVA: 0x0006A41D File Offset: 0x0006861D
			public virtual bool AddOrganizerToSubject
			{
				set
				{
					base.PowerSharpParameters["AddOrganizerToSubject"] = value;
				}
			}

			// Token: 0x170022B2 RID: 8882
			// (set) Token: 0x06003F90 RID: 16272 RVA: 0x0006A435 File Offset: 0x00068635
			public virtual bool DeleteNonCalendarItems
			{
				set
				{
					base.PowerSharpParameters["DeleteNonCalendarItems"] = value;
				}
			}

			// Token: 0x170022B3 RID: 8883
			// (set) Token: 0x06003F91 RID: 16273 RVA: 0x0006A44D File Offset: 0x0006864D
			public virtual bool TentativePendingApproval
			{
				set
				{
					base.PowerSharpParameters["TentativePendingApproval"] = value;
				}
			}

			// Token: 0x170022B4 RID: 8884
			// (set) Token: 0x06003F92 RID: 16274 RVA: 0x0006A465 File Offset: 0x00068665
			public virtual bool EnableResponseDetails
			{
				set
				{
					base.PowerSharpParameters["EnableResponseDetails"] = value;
				}
			}

			// Token: 0x170022B5 RID: 8885
			// (set) Token: 0x06003F93 RID: 16275 RVA: 0x0006A47D File Offset: 0x0006867D
			public virtual bool OrganizerInfo
			{
				set
				{
					base.PowerSharpParameters["OrganizerInfo"] = value;
				}
			}

			// Token: 0x170022B6 RID: 8886
			// (set) Token: 0x06003F94 RID: 16276 RVA: 0x0006A495 File Offset: 0x00068695
			public virtual bool AllRequestOutOfPolicy
			{
				set
				{
					base.PowerSharpParameters["AllRequestOutOfPolicy"] = value;
				}
			}

			// Token: 0x170022B7 RID: 8887
			// (set) Token: 0x06003F95 RID: 16277 RVA: 0x0006A4AD File Offset: 0x000686AD
			public virtual bool AllBookInPolicy
			{
				set
				{
					base.PowerSharpParameters["AllBookInPolicy"] = value;
				}
			}

			// Token: 0x170022B8 RID: 8888
			// (set) Token: 0x06003F96 RID: 16278 RVA: 0x0006A4C5 File Offset: 0x000686C5
			public virtual bool AllRequestInPolicy
			{
				set
				{
					base.PowerSharpParameters["AllRequestInPolicy"] = value;
				}
			}

			// Token: 0x170022B9 RID: 8889
			// (set) Token: 0x06003F97 RID: 16279 RVA: 0x0006A4DD File Offset: 0x000686DD
			public virtual bool AddAdditionalResponse
			{
				set
				{
					base.PowerSharpParameters["AddAdditionalResponse"] = value;
				}
			}

			// Token: 0x170022BA RID: 8890
			// (set) Token: 0x06003F98 RID: 16280 RVA: 0x0006A4F5 File Offset: 0x000686F5
			public virtual string AdditionalResponse
			{
				set
				{
					base.PowerSharpParameters["AdditionalResponse"] = value;
				}
			}

			// Token: 0x170022BB RID: 8891
			// (set) Token: 0x06003F99 RID: 16281 RVA: 0x0006A508 File Offset: 0x00068708
			public virtual bool RemoveOldMeetingMessages
			{
				set
				{
					base.PowerSharpParameters["RemoveOldMeetingMessages"] = value;
				}
			}

			// Token: 0x170022BC RID: 8892
			// (set) Token: 0x06003F9A RID: 16282 RVA: 0x0006A520 File Offset: 0x00068720
			public virtual bool AddNewRequestsTentatively
			{
				set
				{
					base.PowerSharpParameters["AddNewRequestsTentatively"] = value;
				}
			}

			// Token: 0x170022BD RID: 8893
			// (set) Token: 0x06003F9B RID: 16283 RVA: 0x0006A538 File Offset: 0x00068738
			public virtual bool ProcessExternalMeetingMessages
			{
				set
				{
					base.PowerSharpParameters["ProcessExternalMeetingMessages"] = value;
				}
			}

			// Token: 0x170022BE RID: 8894
			// (set) Token: 0x06003F9C RID: 16284 RVA: 0x0006A550 File Offset: 0x00068750
			public virtual bool RemoveForwardedMeetingNotifications
			{
				set
				{
					base.PowerSharpParameters["RemoveForwardedMeetingNotifications"] = value;
				}
			}

			// Token: 0x170022BF RID: 8895
			// (set) Token: 0x06003F9D RID: 16285 RVA: 0x0006A568 File Offset: 0x00068768
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170022C0 RID: 8896
			// (set) Token: 0x06003F9E RID: 16286 RVA: 0x0006A580 File Offset: 0x00068780
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170022C1 RID: 8897
			// (set) Token: 0x06003F9F RID: 16287 RVA: 0x0006A598 File Offset: 0x00068798
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170022C2 RID: 8898
			// (set) Token: 0x06003FA0 RID: 16288 RVA: 0x0006A5B0 File Offset: 0x000687B0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170022C3 RID: 8899
			// (set) Token: 0x06003FA1 RID: 16289 RVA: 0x0006A5C8 File Offset: 0x000687C8
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
