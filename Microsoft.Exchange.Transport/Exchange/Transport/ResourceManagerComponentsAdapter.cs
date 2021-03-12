using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.MessageResubmission;
using Microsoft.Exchange.Transport.Pickup;
using Microsoft.Exchange.Transport.RecipientAPI;
using Microsoft.Exchange.Transport.RemoteDelivery;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000045 RID: 69
	internal class ResourceManagerComponentsAdapter
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000081C0 File Offset: 0x000063C0
		public virtual RemoteDeliveryComponent RemoteDeliveryComponent
		{
			get
			{
				RemoteDeliveryComponent result;
				Components.TryGetRemoteDeliveryComponent(out result);
				return result;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000081D8 File Offset: 0x000063D8
		public virtual IsMemberOfResolverComponent<RoutingAddress> TransportIsMemberOfResolverComponent
		{
			get
			{
				IsMemberOfResolverComponent<RoutingAddress> result;
				Components.TryGetTransportIsMemberOfResolverComponent(out result);
				return result;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000081EE File Offset: 0x000063EE
		public virtual bool IsActive
		{
			get
			{
				return Components.IsActive;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000081F5 File Offset: 0x000063F5
		public virtual bool IsPaused
		{
			get
			{
				return Components.IsPaused;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000081FC File Offset: 0x000063FC
		public virtual bool IsBridgehead
		{
			get
			{
				return Components.IsBridgehead;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00008203 File Offset: 0x00006403
		public virtual object SyncRoot
		{
			get
			{
				return Components.SyncRoot;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000820A File Offset: 0x0000640A
		public virtual bool ShuttingDown
		{
			get
			{
				return Components.ShuttingDown;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00008214 File Offset: 0x00006414
		public virtual ISmtpInComponent SmtpInComponent
		{
			get
			{
				ISmtpInComponent result;
				Components.TryGetSmtpInComponent(out result);
				return result;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000822C File Offset: 0x0000642C
		public virtual IStartableTransportComponent Aggregator
		{
			get
			{
				IStartableTransportComponent result;
				Components.TryGetAggregator(out result);
				return result;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00008244 File Offset: 0x00006444
		public virtual EnhancedDns EnhancedDnsComponent
		{
			get
			{
				EnhancedDns result;
				Components.TryGetEnhancedDnsComponent(out result);
				return result;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000825C File Offset: 0x0000645C
		protected virtual IStartableTransportComponent PickupComponent
		{
			get
			{
				PickupComponent result;
				Components.TryGetPickupComponent(out result);
				return result;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00008274 File Offset: 0x00006474
		protected virtual IStartableTransportComponent StoreDriver
		{
			get
			{
				IStartableTransportComponent result;
				Components.TryGetStoreDriverLoaderComponent(out result);
				return result;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000828C File Offset: 0x0000648C
		protected virtual IStartableTransportComponent BootScanner
		{
			get
			{
				IStartableTransportComponent result;
				Components.TryGetBootScanner(out result);
				return result;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000082A4 File Offset: 0x000064A4
		protected virtual IStartableTransportComponent MessageResubmissionComponent
		{
			get
			{
				MessageResubmissionComponent result;
				Components.TryGetMessageResubmissionComponent(out result);
				return result;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000082BC File Offset: 0x000064BC
		protected virtual IStartableTransportComponent ShadowRedundancyComponent
		{
			get
			{
				ShadowRedundancyComponent result;
				Components.TryGetShadowRedundancyComponent(out result);
				return result;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000082D4 File Offset: 0x000064D4
		public void AddDiagnosticInfo(XElement resourceManagerElement)
		{
			if (resourceManagerElement == null)
			{
				throw new ArgumentNullException("resourceManagerElement");
			}
			XElement xelement = new XElement("CurrentComponentStates");
			XElement xelement2 = new XElement("ResourcePressureRecomendedComponentStates");
			resourceManagerElement.Add(new object[]
			{
				new XElement("serviceActive", this.IsActive),
				new XElement("servicePaused", this.IsPaused),
				new XElement("serviceRole", this.IsBridgehead ? "Hub" : "Edge"),
				new XElement("shuttingDown", this.ShuttingDown),
				xelement2,
				xelement
			});
			foreach (object obj in Enum.GetValues(typeof(ComponentsState)))
			{
				ComponentsState componentsState = (ComponentsState)obj;
				if (ResourceManagerComponentsAdapter.IsSingleComponent(componentsState) && componentsState != ComponentsState.TransportServicePaused)
				{
					string diagnosticNameForComponentEnumFlag = ResourceManagerComponentsAdapter.GetDiagnosticNameForComponentEnumFlag(componentsState);
					xelement.Add(new XElement(diagnosticNameForComponentEnumFlag, ((int)this.currentComponentsState & (int)componentsState) == (int)componentsState));
					xelement2.Add(new XElement(diagnosticNameForComponentEnumFlag, ((int)this.requiredStateDueToBackPressure & (int)componentsState) == (int)componentsState));
				}
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008460 File Offset: 0x00006660
		public override string ToString()
		{
			ComponentsState componentsState = this.currentComponentsState;
			if (componentsState == ComponentsState.AllowAllComponents)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (ComponentsState componentsState2 = ComponentsState.AllowInboundMailSubmissionFromHubs; componentsState2 <= ComponentsState.AllowAllComponents; componentsState2 <<= 1)
			{
				if ((componentsState2 & ComponentsState.AllowAllComponents) != ComponentsState.None && (componentsState2 & componentsState) == ComponentsState.None)
				{
					ComponentsState componentsState3 = componentsState2;
					if (componentsState3 <= ComponentsState.AllowOutboundMailDeliveryToRemoteDomains)
					{
						if (componentsState3 <= ComponentsState.AllowInboundMailSubmissionFromPickupAndReplayDirectory)
						{
							if (componentsState3 < ComponentsState.AllowInboundMailSubmissionFromHubs)
							{
								goto IL_192;
							}
							switch ((int)(componentsState3 - ComponentsState.AllowInboundMailSubmissionFromHubs))
							{
							case 0:
								stringBuilder.AppendLine(Strings.InboundMailSubmissionFromHubsComponent);
								goto IL_1AD;
							case 1:
								stringBuilder.AppendLine(Strings.InboundMailSubmissionFromInternetComponent);
								goto IL_1AD;
							case 2:
								goto IL_192;
							case 3:
								stringBuilder.AppendLine(Strings.InboundMailSubmissionFromPickupDirectoryComponent);
								stringBuilder.AppendLine(Strings.InboundMailSubmissionFromReplayDirectoryComponent);
								goto IL_1AD;
							}
						}
						if (componentsState3 == ComponentsState.AllowInboundMailSubmissionFromMailbox)
						{
							stringBuilder.AppendLine(Strings.InboundMailSubmissionFromMailboxComponent);
							goto IL_1AD;
						}
						if (componentsState3 == ComponentsState.AllowOutboundMailDeliveryToRemoteDomains)
						{
							stringBuilder.AppendLine(Strings.OutboundMailDeliveryToRemoteDomainsComponent);
							goto IL_1AD;
						}
					}
					else if (componentsState3 <= ComponentsState.AllowContentAggregation)
					{
						if (componentsState3 == ComponentsState.AllowBootScannerRunning)
						{
							stringBuilder.AppendLine(Strings.BootScannerComponent);
							goto IL_1AD;
						}
						if (componentsState3 == ComponentsState.AllowContentAggregation)
						{
							if (this.Aggregator != null)
							{
								stringBuilder.AppendLine(Strings.ContentAggregationComponent);
								goto IL_1AD;
							}
							goto IL_1AD;
						}
					}
					else
					{
						if (componentsState3 == ComponentsState.AllowMessageRepositoryResubmission)
						{
							stringBuilder.AppendLine(Strings.MessageResubmissionComponentBanner);
							goto IL_1AD;
						}
						if (componentsState3 == ComponentsState.AllowShadowRedundancyResubmission)
						{
							stringBuilder.AppendLine(Strings.ShadowRedundancyComponentBanner);
							goto IL_1AD;
						}
					}
					IL_192:
					throw new ArgumentException("Unknown component with id = " + componentsState2.ToString());
				}
				IL_1AD:;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008630 File Offset: 0x00006830
		internal bool UpdateComponentsState(ComponentsState requiredComponentState)
		{
			ComponentsState componentsState = requiredComponentState;
			if (this.IsPaused)
			{
				componentsState &= ~(ComponentsState.AllowInboundMailSubmissionFromHubs | ComponentsState.AllowInboundMailSubmissionFromInternet | ComponentsState.AllowInboundMailSubmissionFromPickupAndReplayDirectory | ComponentsState.AllowInboundMailSubmissionFromMailbox | ComponentsState.AllowContentAggregation);
				componentsState |= ComponentsState.TransportServicePaused;
			}
			ComponentsState componentsState2 = this.currentComponentsState ^ componentsState;
			if (componentsState2 == ComponentsState.None)
			{
				return false;
			}
			if (!this.ShuttingDown && this.IsActive)
			{
				lock (this.SyncRoot)
				{
					if (!this.ShuttingDown && this.IsActive)
					{
						this.requiredStateDueToBackPressure = requiredComponentState;
						return this.UpdateComponentsStateInternal(componentsState);
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000086CC File Offset: 0x000068CC
		private static bool IsSingleComponent(ComponentsState componentState)
		{
			return componentState != ComponentsState.None && (componentState & componentState - 1UL) == ComponentsState.None;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000086EC File Offset: 0x000068EC
		private static string GetDiagnosticNameForComponentEnumFlag(ComponentsState state)
		{
			if (state <= ComponentsState.AllowOutboundMailDeliveryToRemoteDomains)
			{
				if (state <= ComponentsState.AllowInboundMailSubmissionFromPickupAndReplayDirectory)
				{
					if (state < ComponentsState.AllowInboundMailSubmissionFromHubs)
					{
						goto IL_A0;
					}
					switch ((int)(state - ComponentsState.AllowInboundMailSubmissionFromHubs))
					{
					case 0:
						return "internalSmtpInEnabled";
					case 1:
						return "internetSmtpInEnabled";
					case 2:
						goto IL_A0;
					case 3:
						return "pickupEnabled";
					}
				}
				if (state == ComponentsState.AllowInboundMailSubmissionFromMailbox)
				{
					return "mailSubmissionEnabled";
				}
				if (state == ComponentsState.AllowOutboundMailDeliveryToRemoteDomains)
				{
					return "remoteDeliveryEnabled";
				}
			}
			else if (state <= ComponentsState.AllowContentAggregation)
			{
				if (state == ComponentsState.AllowBootScannerRunning)
				{
					return "bootScannerEnabled";
				}
				if (state == ComponentsState.AllowContentAggregation)
				{
					return "contentAggregationEnabled";
				}
			}
			else
			{
				if (state == ComponentsState.AllowMessageRepositoryResubmission)
				{
					return "messageRepositoryResubmissionEnabled";
				}
				if (state == ComponentsState.AllowShadowRedundancyResubmission)
				{
					return "shadowRedundancyResubmissionEnabled";
				}
			}
			IL_A0:
			return state.ToString();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000087A4 File Offset: 0x000069A4
		private bool UpdateComponentsStateInternal(ComponentsState requiredComponentState)
		{
			ComponentsState componentsState = this.currentComponentsState ^ requiredComponentState;
			if (componentsState == ComponentsState.None)
			{
				return false;
			}
			this.Populate();
			ComponentsState componentsState2 = ComponentsState.AllowInboundMailSubmissionFromHubs | ComponentsState.AllowInboundMailSubmissionFromInternet | ComponentsState.TransportServicePaused;
			if ((componentsState & componentsState2) != ComponentsState.None)
			{
				ComponentsState componentsState3 = requiredComponentState & ComponentsState.AllowSmtpInComponentToRecvFromInternetAndHubs;
				if (componentsState3 == ComponentsState.AllowSmtpInComponentToRecvFromInternetAndHubs)
				{
					this.SmtpInComponent.Continue();
				}
				else if ((requiredComponentState & ComponentsState.TransportServicePaused) != ComponentsState.None)
				{
					this.SmtpInComponent.Pause();
				}
				else
				{
					bool rejectSubmits = (requiredComponentState & ComponentsState.AllowInboundMailSubmissionFromHubs) == ComponentsState.None;
					this.SmtpInComponent.Pause(rejectSubmits, SmtpResponse.InsufficientResource);
				}
				this.currentComponentsState = ((this.currentComponentsState & ~componentsState2) | (requiredComponentState & componentsState2));
				componentsState &= ~componentsState2;
			}
			ComponentsState componentsState4 = ComponentsState.AllowInboundMailSubmissionFromHubs;
			while (componentsState != ComponentsState.None && componentsState4 != ComponentsState.None)
			{
				if ((componentsState4 & componentsState) != ComponentsState.None)
				{
					IStartableTransportComponent startableTransportComponent;
					if (this.pausableComponents.TryGetValue(componentsState4, out startableTransportComponent))
					{
						if ((requiredComponentState & componentsState4) != ComponentsState.None)
						{
							startableTransportComponent.Continue();
							this.currentComponentsState |= componentsState4;
						}
						else
						{
							startableTransportComponent.Pause();
							this.currentComponentsState &= ~componentsState4;
						}
					}
					componentsState &= ~componentsState4;
				}
				componentsState4 <<= 1;
			}
			return true;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000088A8 File Offset: 0x00006AA8
		private void Populate()
		{
			if (!this.pausableComponentsLoaded)
			{
				this.pausableComponents[ComponentsState.AllowBootScannerRunning] = (this.BootScanner ?? new ResourceManagerComponentsAdapter.MockComponent());
				this.pausableComponents[ComponentsState.AllowContentAggregation] = (this.Aggregator ?? new ResourceManagerComponentsAdapter.MockComponent());
				this.pausableComponents[ComponentsState.AllowInboundMailSubmissionFromMailbox] = (this.StoreDriver ?? new ResourceManagerComponentsAdapter.MockComponent());
				this.pausableComponents[ComponentsState.AllowInboundMailSubmissionFromPickupAndReplayDirectory] = (this.PickupComponent ?? new ResourceManagerComponentsAdapter.MockComponent());
				this.pausableComponents[ComponentsState.AllowOutboundMailDeliveryToRemoteDomains] = (this.RemoteDeliveryComponent ?? new ResourceManagerComponentsAdapter.MockComponent());
				this.pausableComponents[ComponentsState.AllowMessageRepositoryResubmission] = (this.MessageResubmissionComponent ?? new ResourceManagerComponentsAdapter.MockComponent());
				this.pausableComponents[ComponentsState.AllowShadowRedundancyResubmission] = (this.ShadowRedundancyComponent ?? new ResourceManagerComponentsAdapter.MockComponent());
				this.pausableComponentsLoaded = true;
			}
		}

		// Token: 0x04000106 RID: 262
		private ComponentsState currentComponentsState;

		// Token: 0x04000107 RID: 263
		private ComponentsState requiredStateDueToBackPressure;

		// Token: 0x04000108 RID: 264
		private Dictionary<ComponentsState, IStartableTransportComponent> pausableComponents = new Dictionary<ComponentsState, IStartableTransportComponent>(7);

		// Token: 0x04000109 RID: 265
		private bool pausableComponentsLoaded;

		// Token: 0x02000046 RID: 70
		private class MockComponent : IStartableTransportComponent, ITransportComponent
		{
			// Token: 0x17000068 RID: 104
			// (get) Token: 0x060001A7 RID: 423 RVA: 0x000089AD File Offset: 0x00006BAD
			public string CurrentState
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x060001A8 RID: 424 RVA: 0x000089B4 File Offset: 0x00006BB4
			public void Start(bool initiallyPaused, ServiceState targetRunningState)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001A9 RID: 425 RVA: 0x000089BB File Offset: 0x00006BBB
			public void Stop()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001AA RID: 426 RVA: 0x000089C2 File Offset: 0x00006BC2
			public void Pause()
			{
			}

			// Token: 0x060001AB RID: 427 RVA: 0x000089C4 File Offset: 0x00006BC4
			public void Continue()
			{
			}

			// Token: 0x060001AC RID: 428 RVA: 0x000089C6 File Offset: 0x00006BC6
			public void Load()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001AD RID: 429 RVA: 0x000089CD File Offset: 0x00006BCD
			public void Unload()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001AE RID: 430 RVA: 0x000089D4 File Offset: 0x00006BD4
			public string OnUnhandledException(Exception e)
			{
				throw new NotImplementedException();
			}
		}
	}
}
