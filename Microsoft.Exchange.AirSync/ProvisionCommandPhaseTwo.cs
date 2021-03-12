using System;
using System.Globalization;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200010C RID: 268
	internal sealed class ProvisionCommandPhaseTwo : ProvisionCommandPhaseBase
	{
		// Token: 0x06000E7D RID: 3709 RVA: 0x00051573 File Offset: 0x0004F773
		public ProvisionCommandPhaseTwo(IProvisionCommandHost owningCommand) : base(owningCommand)
		{
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0005157C File Offset: 0x0004F77C
		internal override void Process(XmlNode provisionResponseNode)
		{
			ProvisionCommandPhaseTwo.AcknowledgementType acknowledgementType = this.ParseProvisionRequest();
			if ((acknowledgementType & ProvisionCommandPhaseTwo.AcknowledgementType.ForceRemoteWipe) == ProvisionCommandPhaseTwo.AcknowledgementType.ForceRemoteWipe)
			{
				base.GenerateRemoteWipeResponse(provisionResponseNode, ProvisionCommand.ProvisionStatusCode.ProtocolError);
				return;
			}
			bool flag = (acknowledgementType & ProvisionCommandPhaseTwo.AcknowledgementType.RemoteWipe) == ProvisionCommandPhaseTwo.AcknowledgementType.RemoteWipe;
			bool flag2 = (acknowledgementType & ProvisionCommandPhaseTwo.AcknowledgementType.Policy) == ProvisionCommandPhaseTwo.AcknowledgementType.Policy;
			if (flag)
			{
				this.ProcessRemoteWipeAck(provisionResponseNode);
				if (this.remoteWipeServerStatusCode != ProvisionCommandPhaseTwo.RemoteWipeServerStatusCode.Success)
				{
					base.GenerateRemoteWipeResponse(provisionResponseNode, ProvisionCommand.ProvisionStatusCode.ProtocolError);
					return;
				}
				if (!flag2)
				{
					XmlNode xmlNode = this.owningCommand.XmlResponse.CreateElement("Status", "Provision:");
					provisionResponseNode.AppendChild(xmlNode);
					xmlNode.InnerText = 1.ToString(CultureInfo.InvariantCulture);
					return;
				}
			}
			if (flag2)
			{
				this.ProcessPolicy(provisionResponseNode);
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00051610 File Offset: 0x0004F810
		private ProvisionCommandPhaseTwo.AcknowledgementType ParseProvisionRequest()
		{
			ProvisionCommandPhaseTwo.AcknowledgementType acknowledgementType = ProvisionCommandPhaseTwo.AcknowledgementType.None;
			XmlNode xmlRequest = base.XmlRequest;
			uint num = 0U;
			XmlNode xmlNode = xmlRequest["RemoteWipe", "Provision:"];
			if (xmlNode != null)
			{
				if (!base.RemoteWipeRequired)
				{
					throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, false)
					{
						ErrorStringForProtocolLogger = "RemoteWipeWasNotRequested"
					};
				}
				XmlNode xmlNode2 = xmlNode["Status", "Provision:"];
				if (xmlNode2 != null)
				{
					if (!uint.TryParse(xmlNode2.InnerText, out num) || num < 1U || num > 2U)
					{
						this.requestRemoteWipeStatus = ProvisionCommandPhaseTwo.RemoteWipeStatusCodeFromClient.Invalid;
					}
					else
					{
						this.requestRemoteWipeStatus = (ProvisionCommandPhaseTwo.RemoteWipeStatusCodeFromClient)num;
					}
				}
				acknowledgementType = ProvisionCommandPhaseTwo.AcknowledgementType.RemoteWipe;
			}
			if (base.RemoteWipeRequired && this.requestRemoteWipeStatus == ProvisionCommandPhaseTwo.RemoteWipeStatusCodeFromClient.NotPresent)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[ProvisionCommandPhaseTwo.ParseProvisionRequest] Client responded to policy ack, but there is a remoteWipe request pending.");
				return ProvisionCommandPhaseTwo.AcknowledgementType.ForceRemoteWipe;
			}
			XmlNode xmlNode3 = xmlRequest["Policies", "Provision:"];
			if (xmlNode3 != null)
			{
				XmlNode xmlNode4 = xmlNode3["Policy", "Provision:"];
				XmlNode xmlNode5 = xmlNode4["PolicyKey", "Provision:"];
				XmlNode xmlNode6 = xmlNode4["Status", "Provision:"];
				if (xmlNode5 != null && uint.TryParse(xmlNode5.InnerText, out num))
				{
					this.requestPolicyKey = new uint?(num);
				}
				XmlNode xmlNode7 = xmlNode4["PolicyType", "Provision:"];
				this.requestPolicyType = xmlNode7.InnerText;
				if (xmlNode6 != null)
				{
					if (!uint.TryParse(xmlNode6.InnerText, out num) || num < 1U || num > 4U)
					{
						this.requestPolicyStatus = ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.Invalid;
					}
					else
					{
						this.requestPolicyStatus = (ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient)num;
					}
					this.owningCommand.ProtocolLogger.SetValue(ProtocolLoggerData.PolicyAckStatus, (int)this.requestPolicyStatus);
				}
				acknowledgementType |= ProvisionCommandPhaseTwo.AcknowledgementType.Policy;
			}
			return acknowledgementType;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0005179C File Offset: 0x0004F99C
		private void ProcessPolicy(XmlNode response)
		{
			uint? headerPolicyKey = this.owningCommand.HeaderPolicyKey;
			this.owningCommand.GlobalInfo.ProvisionSupported = true;
			bool flag;
			Command.DetectPolicyChange(this.owningCommand.PolicyData, this.owningCommand.GlobalInfo, this.owningCommand.ProtocolVersion, out flag);
			if (!flag)
			{
				this.owningCommand.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeviceNotFullyProvisionable");
				this.owningCommand.SetErrorResponse(HttpStatusCode.Forbidden, StatusCode.DeviceNotFullyProvisionable);
				this.owningCommand.GlobalInfo.DevicePolicyApplicationStatus = DevicePolicyApplicationStatus.NotApplied;
				return;
			}
			if (!string.IsNullOrEmpty(this.requestPolicyType) && this.requestPolicyStatus != ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.NotPresent && this.requestPolicyKey != null)
			{
				if (!ProvisionCommandPhaseBase.IsValidPolicyType(this.requestPolicyType))
				{
					this.owningCommand.GlobalInfo.DevicePolicyApplicationStatus = DevicePolicyApplicationStatus.NotApplied;
					this.responsePolicyType = this.requestPolicyType;
					this.responsePolicyStatus = ProvisionCommand.PolicyStatusCode.UnknownPolicyType;
				}
				else
				{
					if (this.requestPolicyStatus < ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.MinValue || this.requestPolicyStatus > ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.AllowExternalDeviceManagement)
					{
						this.owningCommand.GlobalInfo.DevicePolicyApplicationStatus = DevicePolicyApplicationStatus.NotApplied;
						this.responseProvisionStatus = ProvisionCommand.ProvisionStatusCode.ProtocolError;
						this.BuildPolicyResponse(response);
						return;
					}
					if (this.requestPolicyStatus >= ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.AllowExternalDeviceManagement && this.owningCommand.ProtocolVersion < 121)
					{
						this.responseProvisionStatus = ProvisionCommand.ProvisionStatusCode.ProtocolError;
						this.owningCommand.GlobalInfo.DevicePolicyApplicationStatus = DevicePolicyApplicationStatus.NotApplied;
						this.BuildPolicyResponse(response);
						return;
					}
					if (this.requestPolicyKey == null)
					{
						this.owningCommand.GlobalInfo.DevicePolicyApplicationStatus = DevicePolicyApplicationStatus.NotApplied;
						this.responseProvisionStatus = ProvisionCommand.ProvisionStatusCode.ProtocolError;
						this.BuildPolicyResponse(response);
						return;
					}
					if (this.requestPolicyKey == this.owningCommand.GlobalInfo.PolicyKeyWaitingAck)
					{
						this.responsePolicyType = this.requestPolicyType;
						this.responsePolicyStatus = ProvisionCommand.PolicyStatusCode.Success;
						this.owningCommand.GlobalInfo.DevicePolicyApplicationStatus = ProvisionCommandPhaseTwo.MapPolicyStatusCodeFromClientToDevicePolicyApplicationStatus(this.requestPolicyStatus);
						IPolicyData policyData = this.owningCommand.PolicyData;
						if (policyData != null)
						{
							if (this.requestPolicyStatus == ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.AllowExternalDeviceManagement && !policyData.AllowExternalDeviceManagement)
							{
								this.owningCommand.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ExternallyManagedDevicesNotAllowed");
								this.owningCommand.SetErrorResponse(HttpStatusCode.Forbidden, StatusCode.ExternallyManagedDevicesNotAllowed);
								return;
							}
							if (this.requestPolicyStatus == ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.MinValue || this.requestPolicyStatus == ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.AllowExternalDeviceManagement || (this.requestPolicyStatus == ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.PartialError && policyData.AllowNonProvisionableDevices))
							{
								this.responsePolicyKey = new uint?(this.owningCommand.GlobalInfo.PolicyKeyNeeded);
								this.owningCommand.GlobalInfo.LastPolicyTime = new ExDateTime?(ExDateTime.UtcNow);
								this.owningCommand.GlobalInfo.PolicyKeyOnDevice = this.owningCommand.GlobalInfo.PolicyKeyNeeded;
							}
							else if (this.requestPolicyStatus == ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.PartialError && !policyData.AllowNonProvisionableDevices)
							{
								this.owningCommand.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DevicePartiallyProvisionableStrictPolicy");
								this.owningCommand.SetErrorResponse(HttpStatusCode.Forbidden, StatusCode.DeviceNotFullyProvisionable);
								this.owningCommand.GlobalInfo.DevicePolicyApplicationStatus = DevicePolicyApplicationStatus.NotApplied;
								return;
							}
						}
						else
						{
							this.responsePolicyKey = new uint?(0U);
							this.owningCommand.GlobalInfo.PolicyKeyOnDevice = 0U;
							this.owningCommand.GlobalInfo.LastPolicyTime = new ExDateTime?(ExDateTime.UtcNow);
						}
					}
					else
					{
						this.responsePolicyStatus = ProvisionCommand.PolicyStatusCode.PolicyKeyMismatch;
						this.responsePolicyType = this.requestPolicyType;
					}
				}
				this.responseProvisionStatus = ProvisionCommand.ProvisionStatusCode.Success;
				this.BuildPolicyResponse(response);
				return;
			}
			this.owningCommand.GlobalInfo.DevicePolicyApplicationStatus = DevicePolicyApplicationStatus.NotApplied;
			this.responseProvisionStatus = ProvisionCommand.ProvisionStatusCode.ProtocolError;
			this.BuildPolicyResponse(response);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00051B24 File Offset: 0x0004FD24
		private void BuildPolicyResponse(XmlNode provisionNode)
		{
			AirSyncDiagnostics.Assert(this.responseProvisionStatus != ProvisionCommand.ProvisionStatusCode.NotPresent);
			XmlNode xmlNode = this.owningCommand.XmlResponse.CreateElement("Status", "Provision:");
			XmlNode xmlNode2 = xmlNode;
			int num = (int)this.responseProvisionStatus;
			xmlNode2.InnerText = num.ToString(CultureInfo.InvariantCulture);
			provisionNode.AppendChild(xmlNode);
			this.owningCommand.ProtocolLogger.SetValue(ProtocolLoggerData.StatusCode, (int)this.responseProvisionStatus);
			if (this.responseProvisionStatus == ProvisionCommand.ProvisionStatusCode.Success && this.responsePolicyType != null)
			{
				XmlNode xmlNode3 = this.owningCommand.XmlResponse.CreateElement("Policies", "Provision:");
				provisionNode.AppendChild(xmlNode3);
				XmlNode xmlNode4 = this.owningCommand.XmlResponse.CreateElement("Policy", "Provision:");
				xmlNode3.AppendChild(xmlNode4);
				XmlNode xmlNode5 = this.owningCommand.XmlResponse.CreateElement("PolicyType", "Provision:");
				xmlNode5.InnerText = this.responsePolicyType;
				xmlNode4.AppendChild(xmlNode5);
				AirSyncDiagnostics.Assert(this.responsePolicyStatus != ProvisionCommand.PolicyStatusCode.NotPresent);
				XmlNode xmlNode6 = this.owningCommand.XmlResponse.CreateElement("Status", "Provision:");
				XmlNode xmlNode7 = xmlNode6;
				int num2 = (int)this.responsePolicyStatus;
				xmlNode7.InnerText = num2.ToString(CultureInfo.InvariantCulture);
				xmlNode4.AppendChild(xmlNode6);
				if (this.responsePolicyKey != null)
				{
					XmlNode xmlNode8 = this.owningCommand.XmlResponse.CreateElement("PolicyKey", "Provision:");
					xmlNode8.InnerText = this.responsePolicyKey.ToString();
					xmlNode4.AppendChild(xmlNode8);
				}
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00051CC0 File Offset: 0x0004FEC0
		private void ProcessRemoteWipeAck(XmlNode response)
		{
			if (this.requestRemoteWipeStatus == ProvisionCommandPhaseTwo.RemoteWipeStatusCodeFromClient.MinValue)
			{
				this.owningCommand.GlobalInfo.RemoteWipeAckTime = new ExDateTime?(ExDateTime.UtcNow);
				this.remoteWipeServerStatusCode = ProvisionCommandPhaseTwo.RemoteWipeServerStatusCode.Success;
				this.owningCommand.SendRemoteWipeConfirmationMessage(ExDateTime.Now);
				return;
			}
			AirSyncDiagnostics.TraceDebug<ProvisionCommandPhaseTwo.RemoteWipeStatusCodeFromClient>(ExTraceGlobals.RequestsTracer, this, "[ProvisionCommandPhaseTwo.ProcessRemoteWipeAck] Client returned {0} for RemoteWipe status.  Failing Provision.", this.requestRemoteWipeStatus);
			this.remoteWipeServerStatusCode = ProvisionCommandPhaseTwo.RemoteWipeServerStatusCode.Failure;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00051D28 File Offset: 0x0004FF28
		private static DevicePolicyApplicationStatus MapPolicyStatusCodeFromClientToDevicePolicyApplicationStatus(ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient statusCode)
		{
			switch (statusCode)
			{
			case ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.MinValue:
				return DevicePolicyApplicationStatus.AppliedInFull;
			case ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.PartialError:
				return DevicePolicyApplicationStatus.PartiallyApplied;
			case ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.PolicyIgnored:
				return DevicePolicyApplicationStatus.NotApplied;
			case ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient.AllowExternalDeviceManagement:
				return DevicePolicyApplicationStatus.ExternallyManaged;
			default:
				return DevicePolicyApplicationStatus.Unknown;
			}
		}

		// Token: 0x040009CC RID: 2508
		private uint? responsePolicyKey;

		// Token: 0x040009CD RID: 2509
		private ProvisionCommandPhaseTwo.RemoteWipeServerStatusCode remoteWipeServerStatusCode;

		// Token: 0x040009CE RID: 2510
		private ProvisionCommand.PolicyStatusCode responsePolicyStatus;

		// Token: 0x040009CF RID: 2511
		private string responsePolicyType;

		// Token: 0x040009D0 RID: 2512
		private ProvisionCommand.ProvisionStatusCode responseProvisionStatus;

		// Token: 0x040009D1 RID: 2513
		private string requestPolicyType;

		// Token: 0x040009D2 RID: 2514
		private uint? requestPolicyKey;

		// Token: 0x040009D3 RID: 2515
		private ProvisionCommandPhaseTwo.PolicyStatusCodeFromClient requestPolicyStatus;

		// Token: 0x040009D4 RID: 2516
		private ProvisionCommandPhaseTwo.RemoteWipeStatusCodeFromClient requestRemoteWipeStatus;

		// Token: 0x0200010D RID: 269
		[Flags]
		private enum AcknowledgementType
		{
			// Token: 0x040009D6 RID: 2518
			None = 0,
			// Token: 0x040009D7 RID: 2519
			Policy = 1,
			// Token: 0x040009D8 RID: 2520
			RemoteWipe = 2,
			// Token: 0x040009D9 RID: 2521
			ForceRemoteWipe = 4
		}

		// Token: 0x0200010E RID: 270
		private enum PolicyStatusCodeFromClient
		{
			// Token: 0x040009DB RID: 2523
			NotPresent,
			// Token: 0x040009DC RID: 2524
			MinValue,
			// Token: 0x040009DD RID: 2525
			Success = 1,
			// Token: 0x040009DE RID: 2526
			PartialError,
			// Token: 0x040009DF RID: 2527
			PolicyIgnored,
			// Token: 0x040009E0 RID: 2528
			AllowExternalDeviceManagement,
			// Token: 0x040009E1 RID: 2529
			MaxValue = 4,
			// Token: 0x040009E2 RID: 2530
			Invalid
		}

		// Token: 0x0200010F RID: 271
		private enum RemoteWipeStatusCodeFromClient
		{
			// Token: 0x040009E4 RID: 2532
			NotPresent,
			// Token: 0x040009E5 RID: 2533
			MinValue,
			// Token: 0x040009E6 RID: 2534
			Success = 1,
			// Token: 0x040009E7 RID: 2535
			Failure,
			// Token: 0x040009E8 RID: 2536
			MaxValue = 2,
			// Token: 0x040009E9 RID: 2537
			Invalid
		}

		// Token: 0x02000110 RID: 272
		private enum RemoteWipeServerStatusCode
		{
			// Token: 0x040009EB RID: 2539
			NotPresent,
			// Token: 0x040009EC RID: 2540
			Success,
			// Token: 0x040009ED RID: 2541
			Failure
		}
	}
}
