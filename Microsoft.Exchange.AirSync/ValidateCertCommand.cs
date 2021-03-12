using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000299 RID: 665
	internal sealed class ValidateCertCommand : Command
	{
		// Token: 0x06001852 RID: 6226 RVA: 0x0008EC18 File Offset: 0x0008CE18
		internal ValidateCertCommand()
		{
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x0008EC54 File Offset: 0x0008CE54
		internal override int MinVersion
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x0008EC58 File Offset: 0x0008CE58
		protected override string RootNodeName
		{
			get
			{
				return "ValidateCert";
			}
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0008EC5F File Offset: 0x0008CE5F
		internal override Command.ExecutionState ExecuteCommand()
		{
			this.ParseXmlRequest();
			this.ProcessCommand();
			this.BuildXmlResponse();
			return Command.ExecutionState.Complete;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0008EC74 File Offset: 0x0008CE74
		protected override bool HandleQuarantinedState()
		{
			this.globalStatus = "17";
			this.BuildXmlResponse();
			return false;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0008EC88 File Offset: 0x0008CE88
		internal override XmlDocument GetValidationErrorXml()
		{
			if (ValidateCertCommand.validationErrorXml == null)
			{
				XmlDocument commandXmlStub = base.GetCommandXmlStub();
				XmlElement xmlElement = commandXmlStub.CreateElement("Status", this.RootNodeNamespace);
				xmlElement.InnerText = "2";
				commandXmlStub[this.RootNodeName].AppendChild(xmlElement);
				ValidateCertCommand.validationErrorXml = commandXmlStub;
			}
			return ValidateCertCommand.validationErrorXml;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0008ECE0 File Offset: 0x0008CEE0
		private void ParseCertNodes(XmlNode containerNode, List<string> certList)
		{
			foreach (object obj in containerNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				certList.Add(xmlNode.InnerText);
				if (certList.Count >= GlobalSettings.MaxRetrievedItems)
				{
					throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, this.GetValidationErrorXml(), null, false)
					{
						ErrorStringForProtocolLogger = "VCTooManyCertificates"
					};
				}
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0008ED68 File Offset: 0x0008CF68
		private void ParseXmlRequest()
		{
			for (XmlNode xmlNode = base.XmlRequest.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.LocalName == "CertificateChain")
				{
					base.ProtocolLogger.SetValue(ProtocolLoggerData.VCertChains, xmlNode.ChildNodes.Count);
					this.ParseCertNodes(xmlNode, this.certChainCerts);
				}
				else if (xmlNode.LocalName == "Certificates")
				{
					base.ProtocolLogger.SetValue(ProtocolLoggerData.VCerts, xmlNode.ChildNodes.Count);
					this.ParseCertNodes(xmlNode, this.endCerts);
				}
				else
				{
					if (!(xmlNode.LocalName == "CheckCrl"))
					{
						throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, this.GetValidationErrorXml(), null, false)
						{
							ErrorStringForProtocolLogger = "BadNode(" + xmlNode.LocalName + ")InValidateCert"
						};
					}
					base.ProtocolLogger.SetValue(ProtocolLoggerData.VCertCRL, 1);
					this.checkCRL = (xmlNode.InnerText == "1");
				}
			}
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0008EE6C File Offset: 0x0008D06C
		private void ExtendCertChainFromConfiguration()
		{
			if (this.smimeCertCAFullLoaded)
			{
				return;
			}
			if (this.SmimeConfiguration != null)
			{
				string text = this.SmimeConfiguration.SMIMECertificateIssuingCAFull();
				if (!string.IsNullOrWhiteSpace(text))
				{
					this.trustedCerts.Add(text);
					this.smimeCertCAFullLoaded = true;
				}
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0008EEB4 File Offset: 0x0008D0B4
		private void ProcessCommand()
		{
			this.globalStatus = "1";
			try
			{
				this.ExtendCertChainFromConfiguration();
				bool enabled = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled;
				this.perCertStatuses = CertificateManager.ValidateCertificates(this.trustedCerts, this.certChainCerts, this.endCerts, this.checkCRL, this.GetHashCode(), base.MailboxLogger, this.smimeCertCAFullLoaded || enabled, base.User.OrganizationId.ToString());
			}
			catch (LocalizedException ex)
			{
				if (base.MailboxLogger != null)
				{
					base.MailboxLogger.SetData(MailboxLogDataName.ValidateCertCommand_ProcessCommand_Exception, ex.ToString());
				}
				AirSyncDiagnostics.TraceDebug<LocalizedException>(ExTraceGlobals.RequestsTracer, this, "Failed to validate certificate: '{0}'", ex);
				this.globalStatus = "17";
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x0008EF88 File Offset: 0x0008D188
		private SmimeConfigurationContainer SmimeConfiguration
		{
			get
			{
				if (this.smimeConfiguration == null)
				{
					this.smimeConfiguration = CertificateManager.LoadSmimeConfiguration(base.User.OrganizationId, this.GetHashCode());
				}
				return this.smimeConfiguration;
			}
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0008EFB4 File Offset: 0x0008D1B4
		private void BuildXmlResponse()
		{
			base.XmlResponse = new SafeXmlDocument();
			this.validateCertNode = base.XmlResponse.CreateElement("ValidateCert", "ValidateCert:");
			base.XmlResponse.AppendChild(this.validateCertNode);
			XmlElement xmlElement = base.XmlResponse.CreateElement("Status", "ValidateCert:");
			xmlElement.InnerText = this.globalStatus;
			this.validateCertNode.AppendChild(xmlElement);
			foreach (ChainValidityStatus status in this.perCertStatuses)
			{
				XmlElement xmlElement2 = base.XmlResponse.CreateElement("Certificate", "ValidateCert:");
				this.validateCertNode.AppendChild(xmlElement2);
				XmlElement xmlElement3 = base.XmlResponse.CreateElement("Status", "ValidateCert:");
				xmlElement3.InnerText = this.CheckStatus(status);
				xmlElement2.AppendChild(xmlElement3);
			}
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0008F0B8 File Offset: 0x0008D2B8
		private string CheckStatus(ChainValidityStatus status)
		{
			if (status <= (ChainValidityStatus)2148081683U)
			{
				switch (status)
				{
				case ChainValidityStatus.Valid:
				case ChainValidityStatus.ValidSelfSigned:
					return "1";
				case ChainValidityStatus.EmptyCertificate:
					return "10";
				default:
					switch (status)
					{
					case (ChainValidityStatus)2148081680U:
						return "13";
					case (ChainValidityStatus)2148081681U:
						goto IL_ED;
					case (ChainValidityStatus)2148081682U:
						break;
					case (ChainValidityStatus)2148081683U:
						return "14";
					default:
						goto IL_ED;
					}
					break;
				}
			}
			else
			{
				if (status == (ChainValidityStatus)2148098052U)
				{
					return "3";
				}
				switch (status)
				{
				case (ChainValidityStatus)2148204801U:
					return "7";
				case (ChainValidityStatus)2148204802U:
					return "8";
				case (ChainValidityStatus)2148204803U:
					return "11";
				case (ChainValidityStatus)2148204804U:
				case (ChainValidityStatus)2148204805U:
				case (ChainValidityStatus)2148204807U:
				case (ChainValidityStatus)2148204808U:
				case (ChainValidityStatus)2148204811U:
					goto IL_ED;
				case (ChainValidityStatus)2148204806U:
					return "9";
				case (ChainValidityStatus)2148204809U:
				case (ChainValidityStatus)2148204813U:
					return "4";
				case (ChainValidityStatus)2148204810U:
					return "5";
				case (ChainValidityStatus)2148204812U:
					return "15";
				case (ChainValidityStatus)2148204814U:
					break;
				case (ChainValidityStatus)2148204815U:
					return "12";
				case (ChainValidityStatus)2148204816U:
					return "6";
				default:
					goto IL_ED;
				}
			}
			return "16";
			IL_ED:
			AirSyncDiagnostics.TraceDebug<ChainValidityStatus>(ExTraceGlobals.RequestsTracer, this, "Unknown status: '{0}'", status);
			return "17";
		}

		// Token: 0x04000F00 RID: 3840
		private static XmlDocument validationErrorXml;

		// Token: 0x04000F01 RID: 3841
		private XmlElement validateCertNode;

		// Token: 0x04000F02 RID: 3842
		private List<string> certChainCerts = new List<string>(10);

		// Token: 0x04000F03 RID: 3843
		private List<string> trustedCerts = new List<string>(10);

		// Token: 0x04000F04 RID: 3844
		private List<string> endCerts = new List<string>(10);

		// Token: 0x04000F05 RID: 3845
		private bool checkCRL;

		// Token: 0x04000F06 RID: 3846
		private string globalStatus;

		// Token: 0x04000F07 RID: 3847
		private List<ChainValidityStatus> perCertStatuses = new List<ChainValidityStatus>(10);

		// Token: 0x04000F08 RID: 3848
		private SmimeConfigurationContainer smimeConfiguration;

		// Token: 0x04000F09 RID: 3849
		private bool smimeCertCAFullLoaded;

		// Token: 0x0200029A RID: 666
		private struct Status
		{
			// Token: 0x04000F0A RID: 3850
			public const string InvalidStatus = "0";

			// Token: 0x04000F0B RID: 3851
			public const string Success = "1";

			// Token: 0x04000F0C RID: 3852
			public const string ProtocolError = "2";

			// Token: 0x04000F0D RID: 3853
			public const string SignatureNotValidated = "3";

			// Token: 0x04000F0E RID: 3854
			public const string FromUntrustedSource = "4";

			// Token: 0x04000F0F RID: 3855
			public const string InvalidCertChain = "5";

			// Token: 0x04000F10 RID: 3856
			public const string InvalidForSigning = "6";

			// Token: 0x04000F11 RID: 3857
			public const string ExpiredOrInvalid = "7";

			// Token: 0x04000F12 RID: 3858
			public const string InvalidTimePeriods = "8";

			// Token: 0x04000F13 RID: 3859
			public const string PurposeError = "9";

			// Token: 0x04000F14 RID: 3860
			public const string MissingInfo = "10";

			// Token: 0x04000F15 RID: 3861
			public const string WrongRole = "11";

			// Token: 0x04000F16 RID: 3862
			public const string NotMatch = "12";

			// Token: 0x04000F17 RID: 3863
			public const string Revoked = "13";

			// Token: 0x04000F18 RID: 3864
			public const string NoServerContact = "14";

			// Token: 0x04000F19 RID: 3865
			public const string ChainRevoked = "15";

			// Token: 0x04000F1A RID: 3866
			public const string NoRevocationStatus = "16";

			// Token: 0x04000F1B RID: 3867
			public const string UnknowServerError = "17";
		}
	}
}
