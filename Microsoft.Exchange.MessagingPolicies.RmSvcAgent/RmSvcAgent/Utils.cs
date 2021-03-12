using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.RightsManagement;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x0200001C RID: 28
	internal static class Utils
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00007410 File Offset: 0x00005610
		static Utils()
		{
			Utils.PreserveHeaderIds.Add(HeaderId.ContentType);
			Utils.PreserveHeaderIds.Add(HeaderId.MimeVersion);
			Utils.PreserveHeaderIds.Add(HeaderId.ContentClass);
			Utils.PreserveHeaderIds.Add(HeaderId.ContentTransferEncoding);
			Utils.PreserveHeaderIds.Add(HeaderId.ContentLanguage);
			Utils.PreserveHeaderNames.Add("X-MS-TNEF-Correlator");
			Utils.PreserveHeaderNames.Add("Accept-Language");
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000074B0 File Offset: 0x000056B0
		public static bool CheckMuaSubmission(MailItem mailItem)
		{
			if (mailItem.InboundDeliveryMethod != DeliveryMethod.Smtp)
			{
				ExTraceGlobals.RmSvcAgentTracer.TraceDebug(0L, "Non-SMTP message, skipping MUA check");
				return false;
			}
			object obj;
			if (!mailItem.Properties.TryGetValue("Microsoft.Exchange.SmtpMuaSubmission", out obj))
			{
				ExTraceGlobals.RmSvcAgentTracer.TraceError<string>(0L, "Unexpected error: the property {0} does not exist on an SMTP submitted message. Assuming this is not an MUA", "Microsoft.Exchange.SmtpMuaSubmission");
				return false;
			}
			if (!(obj is bool))
			{
				ExTraceGlobals.RmSvcAgentTracer.TraceError<string>(0L, "Unexpected error: the property {0} was expected to be a UINT, but it is not. Assuming this is not an MUA", "Microsoft.Exchange.SmtpMuaSubmission");
				return false;
			}
			return (bool)obj;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000752C File Offset: 0x0000572C
		public static int GetMaxActiveAgents()
		{
			int num = Components.TransportAppConfig.Resolver.MaxExecutingJobs;
			num >>= 1;
			if (num >= 1)
			{
				return num;
			}
			return 1;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00007554 File Offset: 0x00005754
		public static bool IsSupportedMapiMessageClass(EmailMessage message)
		{
			if (string.Equals(message.MapiMessageClass, Constants.SupportedMapiMessageClassForDrm, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			ExTraceGlobals.RmSvcAgentTracer.TraceDebug<string>(0L, "Unsupported message class for Drm: {0}", message.MapiMessageClass);
			return false;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00007584 File Offset: 0x00005784
		public static RmsClientManagerContext CreateRmsContext(OrganizationId orgId, MailItem mailItem, string messageId, string publishingLicense = null)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			IReadOnlyMailItem readOnlyMailItem = (IReadOnlyMailItem)((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			if (readOnlyMailItem != null)
			{
				return new RmsClientManagerContext(orgId, RmsClientManagerContext.ContextId.MessageId, messageId, readOnlyMailItem.ADRecipientCache, new RmsLatencyTracker(readOnlyMailItem.LatencyTracker), publishingLicense)
				{
					SystemProbeId = mailItem.SystemProbeId
				};
			}
			return new RmsClientManagerContext(orgId, RmsClientManagerContext.ContextId.MessageId, messageId, null, null, publishingLicense)
			{
				SystemProbeId = mailItem.SystemProbeId
			};
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00007600 File Offset: 0x00005800
		public static void PatchReceiverHeader(HeaderList headerList, string localTcpInfo, string clauseToInsert)
		{
			DateTime t = DateTime.MaxValue;
			ReceivedHeader receivedHeader = null;
			foreach (Header header in headerList)
			{
				if (header.HeaderId == HeaderId.Received)
				{
					ReceivedHeader receivedHeader2 = header as ReceivedHeader;
					DateTime dateTime;
					if (receivedHeader2 != null && receivedHeader2.ByTcpInfo != null && receivedHeader2.ByTcpInfo.Contains(localTcpInfo) && DateTime.TryParse(receivedHeader2.Date, out dateTime) && dateTime < t)
					{
						receivedHeader = receivedHeader2;
						t = dateTime;
					}
				}
			}
			if (receivedHeader != null)
			{
				headerList.RemoveChild(receivedHeader);
				int num = (receivedHeader.With == null) ? -1 : receivedHeader.With.IndexOf(')');
				StringBuilder stringBuilder = new StringBuilder(clauseToInsert.Length + 3);
				string with;
				if (num == -1)
				{
					stringBuilder.Append(" (");
					stringBuilder.Append(clauseToInsert);
					stringBuilder.Append(")");
					with = receivedHeader.With + stringBuilder.ToString();
				}
				else
				{
					stringBuilder.Append(" + ");
					stringBuilder.Append(clauseToInsert);
					with = receivedHeader.With.Insert(num, stringBuilder.ToString());
				}
				ReceivedHeader newChild = new ReceivedHeader(receivedHeader.From, (receivedHeader.FromTcpInfo == null) ? null : receivedHeader.FromTcpInfo.Trim(Utils.Parentheses), receivedHeader.By, (receivedHeader.ByTcpInfo == null) ? null : receivedHeader.ByTcpInfo.Trim(Utils.Parentheses), receivedHeader.For, with, receivedHeader.Id, receivedHeader.Via, receivedHeader.Date);
				headerList.InsertAfter(newChild, receivedHeader.LastChild);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000077AC File Offset: 0x000059AC
		public static int IncrementDeferralCount(MailItem mailItem, string deferralCountPropertyName)
		{
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			ArgumentValidator.ThrowIfNullOrEmpty("deferralCountPropertyName", deferralCountPropertyName);
			object obj;
			int num;
			if (!mailItem.Properties.TryGetValue(deferralCountPropertyName, out obj))
			{
				num = 0;
			}
			else
			{
				if (!(obj is int))
				{
					return -1;
				}
				num = (int)obj;
			}
			num++;
			mailItem.Properties[deferralCountPropertyName] = num;
			return num;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000780B File Offset: 0x00005A0B
		public static void SetTransportDecryptionApplied(MailItem mailItem, bool reset)
		{
			if (reset)
			{
				if (mailItem.Properties.ContainsKey("Microsoft.Exchange.RightsManagement.TransportDecrypted"))
				{
					mailItem.Properties.Remove("Microsoft.Exchange.RightsManagement.TransportDecrypted");
					return;
				}
			}
			else
			{
				mailItem.Properties["Microsoft.Exchange.RightsManagement.TransportDecrypted"] = "True";
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000784C File Offset: 0x00005A4C
		public static void GetTransportDecryptionPLAndUL(MailItem mailItem, out string publishLicense, out string useLicense)
		{
			object obj;
			mailItem.Properties.TryGetValue("Microsoft.Exchange.RightsManagement.TransportDecryptionPL", out obj);
			publishLicense = (string)obj;
			mailItem.Properties.TryGetValue("Microsoft.Exchange.RightsManagement.TransportDecryptionUL", out obj);
			useLicense = (string)obj;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000788F File Offset: 0x00005A8F
		public static void SetTransportDecryptionPLAndUL(MailItem mailItem, string publishLicense, string useLicense)
		{
			mailItem.Properties["Microsoft.Exchange.RightsManagement.TransportDecryptionPL"] = publishLicense;
			mailItem.Properties["Microsoft.Exchange.RightsManagement.TransportDecryptionUL"] = useLicense;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000078B4 File Offset: 0x00005AB4
		public static string GetDecryptionTokenRecipient(MailItem mailItem, AcceptedDomainCollection acceptedDomains)
		{
			object obj;
			string text;
			if (!mailItem.Properties.TryGetValue("Microsoft.Exchange.RightsManagement.DecryptionTokenRecipient", out obj))
			{
				text = MPCommonUtils.GetDecryptionTokenRecipient(mailItem, acceptedDomains);
				mailItem.Properties["Microsoft.Exchange.RightsManagement.DecryptionTokenRecipient"] = text;
			}
			else
			{
				text = (string)obj;
			}
			return text;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000078F8 File Offset: 0x00005AF8
		public static void NDRMessage(MailItem mailItem, string messageId, HashSet<int> recipientsToNDR, SmtpResponse response)
		{
			EnvelopeRecipientCollection recipients = mailItem.Recipients;
			if (recipients == null)
			{
				ExTraceGlobals.RmSvcAgentTracer.TraceError<string>(0L, "No recipients to NDR for message {0}", messageId);
				return;
			}
			for (int i = recipients.Count - 1; i >= 0; i--)
			{
				if (recipientsToNDR.Contains(i))
				{
					ExTraceGlobals.RmSvcAgentTracer.TraceDebug<string, string[]>(0L, "NDR recipient {0} with {1}", recipients[i].Address.ToString(), response.StatusText);
					mailItem.Recipients.Remove(recipients[i], DsnType.Failure, response);
				}
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00007988 File Offset: 0x00005B88
		public static void NDRMessage(MailItem mailItem, string messageId, SmtpResponse response)
		{
			ExTraceGlobals.RmSvcAgentTracer.TraceError<string, SmtpResponse>(0L, "NDRMessage for message {0}, Response {1}", messageId, response);
			EnvelopeRecipientCollection recipients = mailItem.Recipients;
			if (recipients == null)
			{
				ExTraceGlobals.RmSvcAgentTracer.TraceError<string>(0L, "No recipients to NDR for message {0}", messageId);
				return;
			}
			for (int i = recipients.Count - 1; i >= 0; i--)
			{
				mailItem.Recipients.Remove(recipients[i], DsnType.Failure, response);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000079ED File Offset: 0x00005BED
		public static SmtpResponse GetResponseForExceptionDeferral(Exception exception, string[] additionalInfo)
		{
			return Utils.GetResponseForDeferral(Utils.GetSmtpResponseTextsForException(exception, additionalInfo));
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000079FB File Offset: 0x00005BFB
		public static SmtpResponse GetResponseForDeferral(string[] text)
		{
			return new SmtpResponse("451", "4.3.2", Utils.FilterAsciiStrings(text));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00007A14 File Offset: 0x00005C14
		public static SmtpResponse GetResponseForNDR(string[] text)
		{
			string[] array = Utils.FilterAsciiStrings(text);
			if (array.Length == 0)
			{
				return Constants.NDRResponse;
			}
			List<string> list = new List<string>(text.Length + 2);
			list.Add("Delivery not authorized, message refused.");
			list.AddRange(array);
			list.Add("Please contact your system administrator for more information.");
			return new SmtpResponse("550", "5.7.1", list.ToArray());
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00007A70 File Offset: 0x00005C70
		public static string[] GetSmtpResponseTextsForException(Exception exception, string[] additionalInfo)
		{
			if (exception == null)
			{
				return additionalInfo;
			}
			List<string> list;
			if (additionalInfo != null && additionalInfo.Length != 0)
			{
				list = new List<string>(2 + additionalInfo.Length);
			}
			else
			{
				list = new List<string>(2);
			}
			RightsManagementException ex = exception as RightsManagementException;
			if (ex != null)
			{
				list.AddRange(new string[]
				{
					string.Format(CultureInfo.InvariantCulture, "Exception encountered: {0}.", new object[]
					{
						exception.GetType().Name
					}),
					string.Format(CultureInfo.InvariantCulture, "Failure Code: {0}.", new object[]
					{
						ex.FailureCode.ToString()
					})
				});
			}
			else
			{
				list.AddRange(new string[]
				{
					string.Format(CultureInfo.InvariantCulture, "Exception encountered: {0}.", new object[]
					{
						exception.GetType()
					})
				});
			}
			if (additionalInfo != null && additionalInfo.Length != 0)
			{
				list.AddRange(additionalInfo);
			}
			return list.ToArray();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00007B5C File Offset: 0x00005D5C
		private static string[] FilterAsciiStrings(string[] texts)
		{
			if (texts == null)
			{
				return null;
			}
			List<string> list = new List<string>(texts.Length);
			foreach (string text in texts)
			{
				AsciiString arg;
				if (AsciiString.TryParse(text, out arg))
				{
					list.Add(text);
				}
				else
				{
					ExTraceGlobals.RmSvcAgentTracer.TraceError<AsciiString>(0L, "Encountered a Non-ASCII string in the response. String {0} - filtering it out of SmtpResponse", arg);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00007BBC File Offset: 0x00005DBC
		public static string GetTenantString(Guid tenantId)
		{
			string result = "Enterprise";
			if (tenantId != Guid.Empty)
			{
				result = string.Format(CultureInfo.InvariantCulture, "Tenant '{0}'", new object[]
				{
					tenantId
				});
			}
			return result;
		}

		// Token: 0x040000E5 RID: 229
		public const string TransportDecryptionApplied = "Transport Decrypted";

		// Token: 0x040000E6 RID: 230
		private static readonly char[] Parentheses = new char[]
		{
			'(',
			')'
		};

		// Token: 0x040000E7 RID: 231
		internal static readonly HashSet<HeaderId> PreserveHeaderIds = new HashSet<HeaderId>();

		// Token: 0x040000E8 RID: 232
		internal static readonly HashSet<string> PreserveHeaderNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
	}
}
