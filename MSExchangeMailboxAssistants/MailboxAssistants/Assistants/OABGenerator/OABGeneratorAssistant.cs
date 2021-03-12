using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.OAB;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001DD RID: 477
	internal sealed class OABGeneratorAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x06001243 RID: 4675 RVA: 0x0006875D File Offset: 0x0006695D
		public OABGeneratorAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			OABGeneratorAssistant.Tracer.TraceFunction((long)this.GetHashCode(), "OABGeneratorAssistant.OABGeneratorAssistant");
			this.currentOABsInProcessing = new HashSet<Guid>();
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00068789 File Offset: 0x00066989
		public void OnWorkCycleCheckpoint()
		{
			OABGeneratorAssistant.Tracer.TraceFunction((long)this.GetHashCode(), "OABGeneratorAssistant.OnWorkCycleCheckpoint");
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000687A4 File Offset: 0x000669A4
		public override List<ResourceKey> GetResourceDependencies()
		{
			OABGeneratorAssistant.Tracer.TraceFunction((long)this.GetHashCode(), "OABGeneratorAssistant.GetResourceDependencies");
			List<ResourceKey> resourceDependencies = base.GetResourceDependencies();
			if (!resourceDependencies.Contains(ProcessorResourceKey.Local))
			{
				resourceDependencies.Add(ProcessorResourceKey.Local);
			}
			return resourceDependencies;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000687E8 File Offset: 0x000669E8
		public override MailboxData CreateOnDemandMailboxData(Guid itemGuid, string parameters)
		{
			OABGeneratorAssistantRunNowParameters oabgeneratorAssistantRunNowParameters;
			if (!OABGeneratorAssistantRunNowParameters.TryParse(parameters, out oabgeneratorAssistantRunNowParameters))
			{
				OABGeneratorAssistant.Tracer.TraceError<string>((long)this.GetHashCode(), "OABGeneratorAssistant.CreateOnDemandMailboxData: ignoring on-demand request due malformed string parameter: {0}.", parameters);
				return null;
			}
			ADSessionSettings sessionSettings = OABVariantConfigurationSettings.IsMultitenancyEnabled ? ADSessionSettings.FromAllTenantsPartitionId(oabgeneratorAssistantRunNowParameters.PartitionId) : ADSessionSettings.FromRootOrgScopeSet();
			ADUser organizationalMailboxFromAD = this.GetOrganizationalMailboxFromAD(sessionSettings, itemGuid);
			if (organizationalMailboxFromAD == null)
			{
				OABGeneratorAssistant.Tracer.TraceError<Guid>((long)this.GetHashCode(), "OABGeneratorAssistant.CreateOnDemandMailboxData: ignoring on-demand request due to unknown organization mailbox: {0}", itemGuid);
				return null;
			}
			return new OABGeneratorMailboxData(organizationalMailboxFromAD.OrganizationId, base.DatabaseInfo.Guid, organizationalMailboxFromAD.ExchangeGuid, organizationalMailboxFromAD.DisplayName, organizationalMailboxFromAD.Sid, organizationalMailboxFromAD.PrimarySmtpAddress.Domain, oabgeneratorAssistantRunNowParameters.ObjectGuid, TenantPartitionHint.FromOrganizationId(organizationalMailboxFromAD.OrganizationId), oabgeneratorAssistantRunNowParameters.Description);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00068974 File Offset: 0x00066B74
		public override AssistantTaskContext InitializeContext(MailboxData mailbox, TimeBasedDatabaseJob job)
		{
			OABGeneratorAssistant.<>c__DisplayClass5 CS$<>8__locals1 = new OABGeneratorAssistant.<>c__DisplayClass5();
			CS$<>8__locals1.mailbox = mailbox;
			CS$<>8__locals1.job = job;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.initializedContext = null;
			ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<InitializeContext>b__0)), new FilterDelegate(this, (UIntPtr)ldftn(<InitializeContext>b__3)), new CatchDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<InitializeContext>b__4)));
			return CS$<>8__locals1.initializedContext;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x000689D4 File Offset: 0x00066BD4
		private AssistantTaskContext InitializeContextInternal(MailboxData mailbox, TimeBasedDatabaseJob job)
		{
			OABLogger.LogRecord(TraceType.FunctionTrace, "OABGeneratorAssistant.InitializeContextInternal: start", new object[0]);
			AssistantTaskContext result;
			try
			{
				if (mailbox is OABGeneratorMailboxData)
				{
					result = OABGeneratorTaskContext.FromAssistantTaskContext(new AssistantTaskContext(mailbox, job, null));
				}
				else if (mailbox is StoreMailboxData)
				{
					StoreMailboxData storeMailboxData = (StoreMailboxData)mailbox;
					ADSessionSettings sessionSettings = OABVariantConfigurationSettings.IsMultitenancyEnabled ? ADSessionSettings.FromTenantPartitionHint(storeMailboxData.TenantPartitionHint) : ADSessionSettings.FromRootOrgScopeSet();
					ADUser organizationalMailboxFromAD = this.GetOrganizationalMailboxFromAD(sessionSettings, storeMailboxData.Guid);
					if (organizationalMailboxFromAD == null)
					{
						OABLogger.LogRecord(TraceType.ErrorTrace, "OABGeneratorAssistant.InitializeContext: ignoring scheduled job due to unknown organization mailbox: {0}", new object[]
						{
							storeMailboxData.Guid
						});
						result = null;
					}
					else if (organizationalMailboxFromAD.RecipientTypeDetails != RecipientTypeDetails.ArbitrationMailbox || !organizationalMailboxFromAD.PersistedCapabilities.Contains(Capability.OrganizationCapabilityOABGen))
					{
						OABLogger.LogRecord(TraceType.ErrorTrace, "OABGeneratorAssistant.InitializeContext: The mailbox {0} is not an organizational mailbox with OABGen capability", new object[]
						{
							storeMailboxData.Guid
						});
						result = null;
					}
					else
					{
						OABGeneratorMailboxData mailboxData = new OABGeneratorMailboxData(organizationalMailboxFromAD.OrganizationId, base.DatabaseInfo.Guid, organizationalMailboxFromAD.ExchangeGuid, organizationalMailboxFromAD.DisplayName, organizationalMailboxFromAD.Sid, organizationalMailboxFromAD.PrimarySmtpAddress.Domain, Guid.Empty, storeMailboxData.TenantPartitionHint, string.Empty);
						OABGeneratorTaskContext oabgeneratorTaskContext = OABGeneratorTaskContext.FromAssistantTaskContext(new AssistantTaskContext(mailboxData, job, null));
						oabgeneratorTaskContext.OrganizationMailbox = organizationalMailboxFromAD;
						result = oabgeneratorTaskContext;
					}
				}
				else
				{
					OABLogger.LogRecord(TraceType.ErrorTrace, "OABGeneratorAssistant.InitializeContext: MailboxData is neither StoreMailboxData nor OABGeneratorMailboxData", new object[0]);
					result = null;
				}
			}
			catch (CannotResolveExternalDirectoryOrganizationIdException ex)
			{
				OABLogger.LogRecord(TraceType.ErrorTrace, "OABGeneratorAssistant.InitializeContext: {0}", new object[]
				{
					ex.ToString()
				});
				result = null;
			}
			finally
			{
				OABLogger.LogRecord(TraceType.FunctionTrace, "OABGeneratorAssistant.InitializeContextInternal: finish", new object[0]);
			}
			return result;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00068BAC File Offset: 0x00066DAC
		public override AssistantTaskContext InitialStep(AssistantTaskContext assistantTaskContext)
		{
			OABGeneratorTaskContext oabgeneratorTaskContext = assistantTaskContext as OABGeneratorTaskContext;
			oabgeneratorTaskContext.OABStep = new AssistantStep(this.InitializeOABGeneration);
			oabgeneratorTaskContext.Step = new AssistantStep(this.ProcessAssistantStep);
			return OABGeneratorTaskContext.FromOABGeneratorTaskContext(oabgeneratorTaskContext);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00068FE8 File Offset: 0x000671E8
		public AssistantTaskContext ProcessAssistantStep(AssistantTaskContext assistantTaskContext)
		{
			OABGeneratorAssistant.<>c__DisplayClasse CS$<>8__locals1 = new OABGeneratorAssistant.<>c__DisplayClasse();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.context = (assistantTaskContext as OABGeneratorTaskContext);
			if (CS$<>8__locals1.context == null || CS$<>8__locals1.context.OABStep == null)
			{
				OABLogger.LogRecord(TraceType.ErrorTrace, "OABGeneratorAssistant.ProcessAssistantStep: Null or bad context passed in to OABGeneratorAssistant.ProcessAssistantStep; aborting OAB generation.", new object[0]);
				return null;
			}
			CS$<>8__locals1.nextContext = null;
			ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ProcessAssistantStep>b__9)), new FilterDelegate(this, (UIntPtr)ldftn(<ProcessAssistantStep>b__c)), new CatchDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ProcessAssistantStep>b__d)));
			if (CS$<>8__locals1.nextContext == null || CS$<>8__locals1.nextContext.OABStep == null)
			{
				return null;
			}
			return CS$<>8__locals1.nextContext;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00069084 File Offset: 0x00067284
		public AssistantTaskContext InitializeOABGeneration(AssistantTaskContext assistantTaskContext)
		{
			OABGeneratorTaskContext oabgeneratorTaskContext = assistantTaskContext as OABGeneratorTaskContext;
			AssistantStep oabstep = new AssistantStep(this.BeginProcessingOAB);
			OABLogger.LogRecord(TraceType.FunctionTrace, "OABGeneratorAssistant.InitialStep: start", new object[0]);
			OABGeneratorMailboxData oabgeneratorMailboxData = (OABGeneratorMailboxData)oabgeneratorTaskContext.Args.MailboxData;
			try
			{
				OrganizationId organizationId = oabgeneratorMailboxData.OrganizationId;
				if (organizationId == null)
				{
					throw new ArgumentException("unable to determine organization Id");
				}
				oabgeneratorTaskContext.PerOrgAdSystemConfigSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 502, "InitializeOABGeneration", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\OABGenerator\\OABGeneratorAssistant.cs");
				if (oabgeneratorMailboxData.OfflineAddressBook == Guid.Empty)
				{
					OABLogger.LogRecord(TraceType.InfoTrace, "OABGenerator invoked for scheduled generation of OABs for Org '{0}' on DatabaseGuid={1}", new object[]
					{
						organizationId,
						oabgeneratorTaskContext.Args.MailboxData.DatabaseGuid
					});
					oabgeneratorTaskContext.OABsToGenerate = this.GetOABsFromAD(oabgeneratorTaskContext, Guid.Empty);
				}
				else
				{
					OABLogger.LogRecord(TraceType.InfoTrace, "OABGenerator invoked {0} for on-demand generation of OAB {1} for Org {2} DatabaseGuid={3}, MailboxGuid={4}", new object[]
					{
						oabgeneratorMailboxData.JobDescription,
						oabgeneratorMailboxData.OfflineAddressBook,
						organizationId,
						oabgeneratorTaskContext.Args.MailboxData.DatabaseGuid,
						oabgeneratorMailboxData.MailboxGuid
					});
					oabgeneratorTaskContext.OABsToGenerate = this.GetOABsFromAD(oabgeneratorTaskContext, oabgeneratorMailboxData.OfflineAddressBook);
					if (oabgeneratorTaskContext.OABsToGenerate == null || oabgeneratorTaskContext.OABsToGenerate.Count != 1)
					{
						OABLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_CannotFindOAB, new object[]
						{
							oabgeneratorMailboxData.OfflineAddressBook
						});
					}
				}
				if (oabgeneratorTaskContext.OABsToGenerate == null || oabgeneratorTaskContext.OABsToGenerate.Count == 0)
				{
					OABLogger.LogRecord(TraceType.InfoTrace, "InvokeInternal. No OABs found to generate for DatabaseGuid={0}, MailboxGuid={1}", new object[]
					{
						oabgeneratorMailboxData.DatabaseGuid,
						oabgeneratorMailboxData.MailboxGuid.ToString(),
						oabgeneratorMailboxData.Guid
					});
					if (oabgeneratorTaskContext.Cleanup != null)
					{
						oabgeneratorTaskContext.Cleanup(oabgeneratorTaskContext);
					}
					return null;
				}
				OABLogger.LogRecord(TraceType.InfoTrace, "Found {0} OAB(s) to generate", new object[]
				{
					oabgeneratorTaskContext.OABsToGenerate.Count
				});
				oabgeneratorTaskContext.CurrentOAB = oabgeneratorTaskContext.OABsToGenerate.Dequeue();
			}
			finally
			{
				OABLogger.LogRecord(TraceType.FunctionTrace, "OABGeneratorAssistant.InitialStep: finish", new object[0]);
				oabgeneratorTaskContext.OABStep = oabstep;
			}
			return OABGeneratorTaskContext.FromOABGeneratorTaskContext(oabgeneratorTaskContext);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00069300 File Offset: 0x00067500
		public AssistantTaskContext BeginProcessingOAB(AssistantTaskContext assistantTaskContext)
		{
			OABGeneratorTaskContext oabgeneratorTaskContext = assistantTaskContext as OABGeneratorTaskContext;
			AssistantStep assistantStep = null;
			OABLogger.LogRecord(TraceType.FunctionTrace, "OABGeneratorAssistant.BeginProcessingOAB: start", new object[0]);
			OABGeneratorMailboxData oabgeneratorMailboxData = (OABGeneratorMailboxData)oabgeneratorTaskContext.Args.MailboxData;
			try
			{
				lock (this.currentOABsInProcessing)
				{
					if (this.currentOABsInProcessing.Contains(oabgeneratorTaskContext.CurrentOAB.Id.ObjectGuid))
					{
						OABGeneratorAssistant.Tracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "OABGeneratorAssistant.BeginProcessingOAB: ignoring on-demand request requested OAB is already in processing: {0}.", oabgeneratorTaskContext.CurrentOAB.Id);
						return null;
					}
					this.currentOABsInProcessing.Add(oabgeneratorTaskContext.CurrentOAB.Id.ObjectGuid);
				}
				OABGeneratorTaskContext oabgeneratorTaskContext2 = oabgeneratorTaskContext;
				oabgeneratorTaskContext2.Cleanup = (Action<OABGeneratorTaskContext>)Delegate.Combine(oabgeneratorTaskContext2.Cleanup, new Action<OABGeneratorTaskContext>(this.RemoveCurrentOABFromInProcessingList));
				oabgeneratorTaskContext.OABGenerator = new OABGenerator(oabgeneratorTaskContext.PerOrgAdSystemConfigSession, oabgeneratorTaskContext.CurrentOAB, oabgeneratorMailboxData.MailboxSid, oabgeneratorMailboxData.MailboxDomain, new Action(this.AbortProcessingOnShutdown));
				OABLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_OABGenerationStartGeneration, new object[]
				{
					oabgeneratorTaskContext.CurrentOAB,
					oabgeneratorTaskContext.CurrentOAB.DistinguishedName,
					oabgeneratorTaskContext.CurrentOAB.Id.ObjectGuid
				});
				oabgeneratorTaskContext.OABGenerator.Initialize();
				oabgeneratorTaskContext.ReturnStep.Push(new AssistantStep(this.FinishProcessingOAB));
				assistantStep = new AssistantStep(oabgeneratorTaskContext.OABGenerator.PrepareFilesForOABGeneration);
			}
			finally
			{
				OABLogger.LogRecord(TraceType.FunctionTrace, "OABGeneratorAssistant.BeginProcessingOAB: finish", new object[0]);
				if (assistantStep != null)
				{
					oabgeneratorTaskContext.OABStep = assistantStep;
				}
			}
			return OABGeneratorTaskContext.FromOABGeneratorTaskContext(oabgeneratorTaskContext);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x000694E0 File Offset: 0x000676E0
		public AssistantTaskContext FinishProcessingOAB(AssistantTaskContext assistantTaskContext)
		{
			OABGeneratorTaskContext oabgeneratorTaskContext = assistantTaskContext as OABGeneratorTaskContext;
			OABLogger.LogRecord(TraceType.FunctionTrace, "OABGeneratorAssistant.FinishProcessingOAB: start", new object[0]);
			try
			{
				OABLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_OABGenerationCompletedGeneration, new object[]
				{
					oabgeneratorTaskContext.CurrentOAB.ToString(),
					oabgeneratorTaskContext.CurrentOAB.DistinguishedName,
					oabgeneratorTaskContext.CurrentOAB.Id.ObjectGuid,
					(oabgeneratorTaskContext.OABGenerator.Stats != null) ? oabgeneratorTaskContext.OABGenerator.Stats.GetStringForLogging() : string.Empty
				});
			}
			finally
			{
				if (oabgeneratorTaskContext.Cleanup != null)
				{
					oabgeneratorTaskContext.Cleanup(oabgeneratorTaskContext);
				}
				oabgeneratorTaskContext.ClearPerOABData();
				OABLogger.LogRecord(TraceType.FunctionTrace, "OABGeneratorAssistant.FinishProcessingOAB: finish", new object[0]);
			}
			if (oabgeneratorTaskContext.OABsToGenerate != null && oabgeneratorTaskContext.OABsToGenerate.Count > 0)
			{
				oabgeneratorTaskContext.CurrentOAB = oabgeneratorTaskContext.OABsToGenerate.Dequeue();
				oabgeneratorTaskContext.OABStep = new AssistantStep(this.BeginProcessingOAB);
				return OABGeneratorTaskContext.FromOABGeneratorTaskContext(oabgeneratorTaskContext);
			}
			return null;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x000695F4 File Offset: 0x000677F4
		public void RemoveCurrentOABFromInProcessingList(OABGeneratorTaskContext context)
		{
			if (context.CurrentOAB != null)
			{
				lock (this.currentOABsInProcessing)
				{
					this.currentOABsInProcessing.Remove(context.CurrentOAB.Id.ObjectGuid);
				}
			}
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00069654 File Offset: 0x00067854
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			AssistantTaskContext assistantTaskContext = OABGeneratorTaskContext.FromAssistantTaskContext(new AssistantTaskContext(invokeArgs.MailboxData, null, null)
			{
				Step = new AssistantStep(this.InitialStep)
			});
			this.isRunningFromInvoke = true;
			do
			{
				AssistantTaskContext assistantTaskContext2 = assistantTaskContext;
				assistantTaskContext2.Args = invokeArgs;
				assistantTaskContext = assistantTaskContext2.Step(assistantTaskContext2);
			}
			while (assistantTaskContext != null && assistantTaskContext.Step != null);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x000696B4 File Offset: 0x000678B4
		private ADUser GetOrganizationalMailboxFromAD(ADSessionSettings sessionSettings, Guid mailboxGuid)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 733, "GetOrganizationalMailboxFromAD", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\OABGenerator\\OABGeneratorAssistant.cs");
			ADRecipient adrecipient;
			try
			{
				adrecipient = tenantOrRootOrgRecipientSession.FindByExchangeGuid(mailboxGuid);
			}
			catch (NonUniqueRecipientException)
			{
				OABGeneratorAssistant.Tracer.TraceError<Guid>((long)this.GetHashCode(), "OABGeneratorAssistant.GetOrganizationalMailboxFromAD: multiple mailboxes have ExchangeGuid={0}", mailboxGuid);
				OABLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_OrganizationalMailboxGuidIsNotUnique, new object[]
				{
					mailboxGuid
				});
				return null;
			}
			if (adrecipient == null)
			{
				OABGeneratorAssistant.Tracer.TraceError<Guid>((long)this.GetHashCode(), "OABGeneratorAssistant.GetOrganizationalMailboxFromAD: unable to find user object with ExchangeGuid={0}", mailboxGuid);
				return null;
			}
			ADUser aduser = adrecipient as ADUser;
			if (aduser == null)
			{
				OABGeneratorAssistant.Tracer.TraceError<Guid>((long)this.GetHashCode(), "OABGeneratorAssistant.GetOrganizationalMailboxFromAD: unknown organization mailbox: {0}", mailboxGuid);
				return null;
			}
			return aduser;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00069774 File Offset: 0x00067974
		private Queue<OfflineAddressBook> GetOABsFromAD(OABGeneratorTaskContext context, Guid oabObjectGuid)
		{
			OABGeneratorAssistant.Tracer.TraceFunction<Guid>((long)this.GetHashCode(), "GetOABsFromAD. oabObjectGuid={0}", oabObjectGuid);
			Queue<OfflineAddressBook> queue = new Queue<OfflineAddressBook>();
			if (oabObjectGuid == Guid.Empty)
			{
				OfflineAddressBook[] array = this.GetCandidateOABsFromAD(context) ?? new OfflineAddressBook[0];
				OABLogger.LogRecord(TraceType.InfoTrace, "OABGeneratorAssistant.GetCandidateOABSFromAD() returned {0} oab(s)", new object[]
				{
					array.Length.ToString()
				});
				foreach (OfflineAddressBook offlineAddressBook in array)
				{
					if (!this.OABAddressListsAreValid(offlineAddressBook))
					{
						OABLogger.LogRecord(TraceType.ErrorTrace, "OABGeneratorAssistant.GetOABsFromAD: address list is invalid: {0}.", new object[]
						{
							offlineAddressBook.Id
						});
					}
					else if (this.isRunningFromInvoke)
					{
						queue.Enqueue(offlineAddressBook);
					}
					else if (OABVariantConfigurationSettings.IsGenerateRequestedOABsOnlyEnabled && (offlineAddressBook.LastRequestedTime == null || DateTime.UtcNow - offlineAddressBook.LastRequestedTime.Value >= Globals.LastRequestedTimeGenerationWindow))
					{
						OABLogger.LogRecord(TraceType.ErrorTrace, "OABGeneratorAssistant.GetOABsFromAD: LastRequestedTime for OAB {0} is beyond generation window - LastRequestedTime: {1}, LastRequestedTimeWindow: {2}", new object[]
						{
							offlineAddressBook.Id,
							(offlineAddressBook.LastRequestedTime != null) ? offlineAddressBook.LastRequestedTime.Value.ToString() : "null",
							Globals.LastRequestedTimeGenerationWindow
						});
					}
					else
					{
						queue.Enqueue(offlineAddressBook);
					}
				}
			}
			else
			{
				OfflineAddressBook specificOABFromAD = this.GetSpecificOABFromAD(context, oabObjectGuid);
				if (specificOABFromAD != null && this.OABAddressListsAreValid(specificOABFromAD))
				{
					queue.Enqueue(specificOABFromAD);
				}
			}
			OABLogger.LogRecord(TraceType.InfoTrace, "OABGeneratorAssistant.GetOABsFromAD() returned {0} oab(s)", new object[]
			{
				queue.Count
			});
			return queue;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0006993C File Offset: 0x00067B3C
		private OfflineAddressBook GetSpecificOABFromAD(OABGeneratorTaskContext context, Guid oabObjectGuid)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				OABGeneratorAssistant.versionFilter,
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, oabObjectGuid)
			});
			OfflineAddressBook[] array = context.PerOrgAdSystemConfigSession.Find<OfflineAddressBook>(null, QueryScope.SubTree, filter, null, 1);
			if (array == null || array.Length <= 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00069994 File Offset: 0x00067B94
		private OfflineAddressBook[] GetCandidateOABsFromAD(OABGeneratorTaskContext context)
		{
			if (!OABVariantConfigurationSettings.IsLinkedOABGenMailboxesEnabled)
			{
				QueryFilter filter = OABGeneratorAssistant.versionFilter;
				return context.PerOrgAdSystemConfigSession.Find<OfflineAddressBook>(null, QueryScope.SubTree, filter, null, 0);
			}
			if (context.OrganizationMailbox == null)
			{
				ADSessionSettings sessionSettings = OABVariantConfigurationSettings.IsMultitenancyEnabled ? ADSessionSettings.FromTenantPartitionHint(((OABGeneratorMailboxData)context.MailboxData).TenantPartitionHint) : ADSessionSettings.FromRootOrgScopeSet();
				context.OrganizationMailbox = this.GetOrganizationalMailboxFromAD(sessionSettings, ((OABGeneratorMailboxData)context.MailboxData).Guid);
			}
			if (context.OrganizationMailbox.GeneratedOfflineAddressBooks != null && context.OrganizationMailbox.GeneratedOfflineAddressBooks.Count > 0)
			{
				OABLogger.LogRecord(TraceType.InfoTrace, "OABGeneratorAssistant.GetCandidateOABsFromAD: Found {0} generated offline address books in org mailbox {1}", new object[]
				{
					context.OrganizationMailbox.GeneratedOfflineAddressBooks.Count.ToString(),
					context.OrganizationMailbox.Id
				});
				List<OfflineAddressBook> list = new List<OfflineAddressBook>(context.OrganizationMailbox.GeneratedOfflineAddressBooks.Count);
				foreach (ADObjectId entryId in context.OrganizationMailbox.GeneratedOfflineAddressBooks)
				{
					OfflineAddressBook offlineAddressBook = context.PerOrgAdSystemConfigSession.Read<OfflineAddressBook>(entryId);
					if (offlineAddressBook != null)
					{
						list.Add(offlineAddressBook);
					}
				}
				return list.ToArray();
			}
			OABLogger.LogRecord(TraceType.InfoTrace, "OABGeneratorAssistant.GetCandidateOABsFromAD: No offline address books found in org mailbox {0}", new object[]
			{
				context.OrganizationMailbox.Id
			});
			return null;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00069B14 File Offset: 0x00067D14
		private bool OABAddressListsAreValid(OfflineAddressBook oab)
		{
			bool flag = true;
			ADObjectId adobjectId = null;
			foreach (ADObjectId adobjectId2 in oab.AddressLists)
			{
				if (string.IsNullOrEmpty(adobjectId2.DistinguishedName))
				{
					flag = false;
					adobjectId = adobjectId2;
					OABLogger.LogRecord(TraceType.ErrorTrace, "Address list id {0} is not valid because it does not have a DN", new object[]
					{
						adobjectId2
					});
					break;
				}
				if (!ADObjectId.IsValidDistinguishedName(adobjectId2.DistinguishedName))
				{
					flag = false;
					adobjectId = adobjectId2;
					OABLogger.LogRecord(TraceType.ErrorTrace, "Address list id {0} is not valid because the DN is not validly formed", new object[]
					{
						adobjectId2.DistinguishedName
					});
					break;
				}
				if (adobjectId2.IsDeleted)
				{
					flag = false;
					adobjectId = adobjectId2;
					OABLogger.LogRecord(TraceType.ErrorTrace, "Address list id {0} is not valid because it is a deleted object", new object[]
					{
						adobjectId2.DistinguishedName
					});
					break;
				}
				bool flag2 = adobjectId2.DistinguishedName.IndexOf(",CN=LostAndFound,DC=", StringComparison.OrdinalIgnoreCase) != -1;
				bool flag3 = adobjectId2.DistinguishedName.IndexOf(",CN=LostAndFoundConfig,CN=Configuration,DC=", StringComparison.OrdinalIgnoreCase) != -1;
				if (flag2 || flag3)
				{
					flag = false;
					adobjectId = adobjectId2;
					OABLogger.LogRecord(TraceType.ErrorTrace, "Address list id {0} is not valid because it is a deleted object", new object[]
					{
						adobjectId2.DistinguishedName
					});
					break;
				}
				bool flag4 = adobjectId2.DistinguishedName.IndexOf(GlobalAddressList.RdnGalContainerToOrganization.DistinguishedName, StringComparison.OrdinalIgnoreCase) > 0;
				bool flag5 = adobjectId2.DistinguishedName.IndexOf(AddressList.RdnAlContainerToOrganization.DistinguishedName, StringComparison.OrdinalIgnoreCase) > 0;
				if (!flag4 && !flag5)
				{
					flag = false;
					adobjectId = adobjectId2;
					OABLogger.LogRecord(TraceType.ErrorTrace, "Address list id {0} is not valid because it is not a child or descendant of either the All Global Address Lists or All Address Lists container", new object[]
					{
						adobjectId2.DistinguishedName
					});
					break;
				}
			}
			if (!flag && adobjectId != null)
			{
				OABLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_OABNotProcessedBecauseAddressListIsInvalid, new object[]
				{
					oab.Identity,
					string.IsNullOrEmpty(adobjectId.DistinguishedName) ? adobjectId.ToString() : adobjectId.DistinguishedName
				});
				OABLogger.LogRecord(TraceType.ErrorTrace, "Address list errors found in OAB {0}, address list {1}", new object[]
				{
					oab.Id,
					string.IsNullOrEmpty(adobjectId.DistinguishedName) ? adobjectId.ToString() : adobjectId.DistinguishedName
				});
			}
			return flag;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00069D50 File Offset: 0x00067F50
		private bool ShouldSendWatsonReportFor(Exception e)
		{
			return !(e is ShutdownCalledException) && !(e is AuthzException) && !(e is ADOperationException) && !(e is ADTransientException) && !(e is AITransientException) && !(e is ThreadAbortException) && !(e is OutOfMemoryException);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00069D90 File Offset: 0x00067F90
		private bool ShouldLogAndIgnore(Exception e)
		{
			return !(e is FileLoadException) && !(e is ThreadAbortException) && !(e is OutOfMemoryException) && !(e is StackOverflowException);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00069DB8 File Offset: 0x00067FB8
		private void AbortProcessingOnShutdown()
		{
			if (base.Shutdown)
			{
				throw new ShutdownCalledException();
			}
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00069DE9 File Offset: 0x00067FE9
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00069DF1 File Offset: 0x00067FF1
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00069DF9 File Offset: 0x00067FF9
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000B2B RID: 2859
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;

		// Token: 0x04000B2C RID: 2860
		private readonly HashSet<Guid> currentOABsInProcessing;

		// Token: 0x04000B2D RID: 2861
		private static readonly QueryFilter versionFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.ExchangeVersion, ExchangeObjectVersion.Exchange2012);

		// Token: 0x04000B2E RID: 2862
		private bool isRunningFromInvoke;
	}
}
