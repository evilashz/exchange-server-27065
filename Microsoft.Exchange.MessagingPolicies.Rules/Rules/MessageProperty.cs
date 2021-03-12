using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200002E RID: 46
	internal class MessageProperty : Property
	{
		// Token: 0x06000197 RID: 407 RVA: 0x00008333 File Offset: 0x00006533
		protected MessageProperty(string propertyName, Type type) : base(propertyName, type)
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008340 File Offset: 0x00006540
		public static MessageProperty Create(string propertyName)
		{
			if (propertyName.Equals("Message.Body", StringComparison.OrdinalIgnoreCase))
			{
				return new BodyProperty();
			}
			KeyValuePair<string, Type> keyValuePair;
			if (MessageProperty.knownProperties.TryGetValue(propertyName, out keyValuePair))
			{
				return new MessageProperty(keyValuePair.Key, keyValuePair.Value);
			}
			throw new RulesValidationException(RulesStrings.InvalidPropertyName(propertyName));
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008390 File Offset: 0x00006590
		protected override object OnGetValue(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			string name;
			if ((name = base.Name) != null)
			{
				if (<PrivateImplementationDetails>{EF903228-1903-470D-BC70-9CF84A099F83}.$$method0x600016f-1 == null)
				{
					<PrivateImplementationDetails>{EF903228-1903-470D-BC70-9CF84A099F83}.$$method0x600016f-1 = new Dictionary<string, int>(28)
					{
						{
							"Message.Subject",
							0
						},
						{
							"Message.Sender",
							1
						},
						{
							"Message.MessageId",
							2
						},
						{
							"Message.InReplyTo",
							3
						},
						{
							"Message.References",
							4
						},
						{
							"Message.ReturnPath",
							5
						},
						{
							"Message.Comments",
							6
						},
						{
							"Message.Keywords",
							7
						},
						{
							"Message.ResentMessageId",
							8
						},
						{
							"Message.From",
							9
						},
						{
							"Message.SenderDomain",
							10
						},
						{
							"Message.To",
							11
						},
						{
							"Message.Cc",
							12
						},
						{
							"Message.ToCc",
							13
						},
						{
							"Message.Bcc",
							14
						},
						{
							"Message.ReplyTo",
							15
						},
						{
							"Message.SclValue",
							16
						},
						{
							"Message.AttachmentNames",
							17
						},
						{
							"Message.AttachmentExtensions",
							18
						},
						{
							"Message.AttachmentTypes",
							19
						},
						{
							"Message.MaxAttachmentSize",
							20
						},
						{
							"Message.ContentCharacterSets",
							21
						},
						{
							"Message.EnvelopeFrom",
							22
						},
						{
							"Message.EnvelopeRecipients",
							23
						},
						{
							"Message.Auth",
							24
						},
						{
							"Message.DataClassifications",
							25
						},
						{
							"Message.Size",
							26
						},
						{
							"Message.SenderIp",
							27
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{EF903228-1903-470D-BC70-9CF84A099F83}.$$method0x600016f-1.TryGetValue(name, out num))
				{
					object obj;
					switch (num)
					{
					case 0:
						obj = MessageProperty.GetSubjectProperty(transportRulesEvaluationContext);
						break;
					case 1:
						obj = transportRulesEvaluationContext.Message.Sender;
						break;
					case 2:
						obj = transportRulesEvaluationContext.Message.MessageId;
						break;
					case 3:
						obj = transportRulesEvaluationContext.Message.InReplyTo;
						break;
					case 4:
						obj = transportRulesEvaluationContext.Message.References;
						break;
					case 5:
						obj = transportRulesEvaluationContext.Message.ReturnPath;
						break;
					case 6:
						obj = transportRulesEvaluationContext.Message.Comments;
						break;
					case 7:
						obj = transportRulesEvaluationContext.Message.Keywords;
						break;
					case 8:
						obj = transportRulesEvaluationContext.Message.ResentMessageId;
						break;
					case 9:
						obj = MessageProperty.GetMessageFromValue(transportRulesEvaluationContext);
						break;
					case 10:
						obj = MessageProperty.GetSenderDomainValue(transportRulesEvaluationContext);
						break;
					case 11:
						obj = transportRulesEvaluationContext.Message.To;
						break;
					case 12:
						obj = transportRulesEvaluationContext.Message.Cc;
						break;
					case 13:
						obj = transportRulesEvaluationContext.Message.ToCc;
						break;
					case 14:
						obj = transportRulesEvaluationContext.Message.Bcc;
						break;
					case 15:
						obj = transportRulesEvaluationContext.Message.ReplyTo;
						break;
					case 16:
						obj = transportRulesEvaluationContext.Message.SclValue;
						break;
					case 17:
						obj = transportRulesEvaluationContext.Message.AttachmentNames;
						break;
					case 18:
						obj = transportRulesEvaluationContext.Message.AttachmentExtensions;
						break;
					case 19:
						obj = transportRulesEvaluationContext.Message.AttachmentTypes;
						break;
					case 20:
						obj = transportRulesEvaluationContext.Message.MaxAttachmentSize;
						break;
					case 21:
						obj = transportRulesEvaluationContext.Message.ContentCharacterSets;
						break;
					case 22:
						obj = transportRulesEvaluationContext.Message.EnvelopeFrom;
						break;
					case 23:
						obj = transportRulesEvaluationContext.Message.EnvelopeRecipients;
						break;
					case 24:
						obj = TransportUtils.GetMessageAuth(transportRulesEvaluationContext);
						break;
					case 25:
						obj = transportRulesEvaluationContext.DataClassifications;
						break;
					case 26:
						obj = transportRulesEvaluationContext.Message.Size;
						break;
					case 27:
						obj = TransportUtils.GetOriginalClientIpAddress(transportRulesEvaluationContext.Message);
						break;
					default:
						goto IL_3D2;
					}
					TransportRulesEvaluator.Trace(transportRulesEvaluationContext.TransportRulesTracer, transportRulesEvaluationContext.MailItem, string.Format("{0} property value evaluated as rule condition: '{1}'", base.Name, obj ?? "null"));
					if (transportRulesEvaluationContext.IsTestMessage && obj is IEnumerable<string>)
					{
						TransportRulesEvaluator.Trace(transportRulesEvaluationContext.TransportRulesTracer, transportRulesEvaluationContext.MailItem, string.Format("property is a collection of values: '{0}'", string.Join(",", obj as IEnumerable<string>)));
					}
					return obj;
				}
			}
			IL_3D2:
			throw new InvalidOperationException("Unknown Property Name");
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000087E4 File Offset: 0x000069E4
		internal static string GetDomain(string address)
		{
			if (!string.IsNullOrEmpty(address))
			{
				return new SmtpAddress(address).Domain;
			}
			return string.Empty;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008810 File Offset: 0x00006A10
		internal static List<string> BuildDistinctMultivaluedProperty(string item1, string item2 = null)
		{
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(item1))
			{
				list.Add(item1);
			}
			if (!string.IsNullOrEmpty(item2) && !item2.Equals(item1, StringComparison.OrdinalIgnoreCase))
			{
				list.Add(item2);
			}
			if (!list.Any<string>())
			{
				list.Add(string.Empty);
			}
			return list;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008860 File Offset: 0x00006A60
		private static object GetSenderDomainValue(TransportRulesEvaluationContext context)
		{
			TransportRule transportRule = context.CurrentRule as TransportRule;
			switch (transportRule.SenderAddressLocation)
			{
			case SenderAddressLocation.Header:
				return MessageProperty.BuildDistinctMultivaluedProperty(MessageProperty.GetDomain(context.Message.From), null);
			case SenderAddressLocation.Envelope:
				return MessageProperty.BuildDistinctMultivaluedProperty(MessageProperty.GetDomain(context.Message.EnvelopeFrom), null);
			case SenderAddressLocation.HeaderOrEnvelope:
				return MessageProperty.BuildDistinctMultivaluedProperty(MessageProperty.GetDomain(context.Message.From), MessageProperty.GetDomain(context.Message.EnvelopeFrom));
			default:
				return null;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000088EC File Offset: 0x00006AEC
		internal static object GetMessageFromValue(TransportRulesEvaluationContext context)
		{
			TransportRule transportRule = context.CurrentRule as TransportRule;
			switch (transportRule.SenderAddressLocation)
			{
			case SenderAddressLocation.Header:
				return MessageProperty.BuildDistinctMultivaluedProperty(context.Message.From, null);
			case SenderAddressLocation.Envelope:
				return MessageProperty.BuildDistinctMultivaluedProperty(context.Message.EnvelopeFrom, null);
			case SenderAddressLocation.HeaderOrEnvelope:
				return MessageProperty.BuildDistinctMultivaluedProperty(context.Message.From, context.Message.EnvelopeFrom);
			default:
				return null;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008964 File Offset: 0x00006B64
		private static Dictionary<string, KeyValuePair<string, Type>> InitializeProperties()
		{
			Dictionary<string, KeyValuePair<string, Type>> dictionary = new Dictionary<string, KeyValuePair<string, Type>>(50);
			MessageProperty.AddProperty("Message.Subject", typeof(string), dictionary);
			MessageProperty.AddProperty("Message.Sender", typeof(string), dictionary);
			MessageProperty.AddProperty("Message.MessageId", typeof(string), dictionary);
			MessageProperty.AddProperty("Message.InReplyTo", typeof(string), dictionary);
			MessageProperty.AddProperty("Message.References", typeof(string), dictionary);
			MessageProperty.AddProperty("Message.ReturnPath", typeof(string), dictionary);
			MessageProperty.AddProperty("Message.Comments", typeof(string), dictionary);
			MessageProperty.AddProperty("Message.Keywords", typeof(string), dictionary);
			MessageProperty.AddProperty("Message.ResentMessageId", typeof(string), dictionary);
			MessageProperty.AddProperty("Message.From", typeof(List<string>), dictionary);
			MessageProperty.AddProperty("Message.To", typeof(List<string>), dictionary);
			MessageProperty.AddProperty("Message.Cc", typeof(List<string>), dictionary);
			MessageProperty.AddProperty("Message.ToCc", typeof(List<string>), dictionary);
			MessageProperty.AddProperty("Message.Bcc", typeof(List<string>), dictionary);
			MessageProperty.AddProperty("Message.ReplyTo", typeof(List<string>), dictionary);
			MessageProperty.AddProperty("Message.SclValue", typeof(int), dictionary);
			MessageProperty.AddProperty("Message.AttachmentNames", typeof(List<string>), dictionary);
			MessageProperty.AddProperty("Message.AttachmentExtensions", typeof(List<string>), dictionary);
			MessageProperty.AddProperty("Message.MaxAttachmentSize", typeof(ulong), dictionary);
			MessageProperty.AddProperty("Message.Size", typeof(ulong), dictionary);
			MessageProperty.AddProperty("Message.AttachmentTypes", typeof(List<string>), dictionary);
			MessageProperty.AddProperty("Message.ContentCharacterSets", typeof(List<string>), dictionary);
			MessageProperty.AddProperty("Message.EnvelopeFrom", typeof(string), dictionary);
			MessageProperty.AddProperty("Message.EnvelopeRecipients", typeof(List<string>), dictionary);
			MessageProperty.AddProperty("Message.Auth", typeof(string), dictionary);
			MessageProperty.AddProperty("Message.DataClassifications", typeof(IEnumerable<DiscoveredDataClassification>), dictionary);
			MessageProperty.AddProperty("Message.SenderIp", typeof(IPAddress), dictionary);
			MessageProperty.AddProperty("Message.SenderDomain", typeof(List<string>), dictionary);
			return dictionary;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008BC6 File Offset: 0x00006DC6
		private static void AddProperty(string name, Type type, Dictionary<string, KeyValuePair<string, Type>> properties)
		{
			properties.Add(name, new KeyValuePair<string, Type>(name, type));
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008BD8 File Offset: 0x00006DD8
		private static string GetSubjectProperty(TransportRulesEvaluationContext context)
		{
			string result;
			try
			{
				result = context.Message.Subject.Normalize(NormalizationForm.FormKC);
			}
			catch (ArgumentException arg)
			{
				TransportRulesEvaluator.Trace(context.TransportRulesTracer, context.MailItem, string.Format("Subject normalization has failed {0}", arg));
				result = context.Message.Subject;
			}
			return result;
		}

		// Token: 0x0400013E RID: 318
		internal const string Subject = "Message.Subject";

		// Token: 0x0400013F RID: 319
		private const string Sender = "Message.Sender";

		// Token: 0x04000140 RID: 320
		private const string MessageId = "Message.MessageId";

		// Token: 0x04000141 RID: 321
		private const string InReplyTo = "Message.InReplyTo";

		// Token: 0x04000142 RID: 322
		private const string References = "Message.References";

		// Token: 0x04000143 RID: 323
		private const string ReturnPath = "Message.ReturnPath";

		// Token: 0x04000144 RID: 324
		private const string Comments = "Message.Comments";

		// Token: 0x04000145 RID: 325
		private const string Keywords = "Message.Keywords";

		// Token: 0x04000146 RID: 326
		private const string ResentMessageId = "Message.ResentMessageId";

		// Token: 0x04000147 RID: 327
		private const string From = "Message.From";

		// Token: 0x04000148 RID: 328
		private const string To = "Message.To";

		// Token: 0x04000149 RID: 329
		private const string Cc = "Message.Cc";

		// Token: 0x0400014A RID: 330
		private const string ToCc = "Message.ToCc";

		// Token: 0x0400014B RID: 331
		private const string Bcc = "Message.Bcc";

		// Token: 0x0400014C RID: 332
		private const string ReplyTo = "Message.ReplyTo";

		// Token: 0x0400014D RID: 333
		private const string SclValue = "Message.SclValue";

		// Token: 0x0400014E RID: 334
		private const string EnvelopeFrom = "Message.EnvelopeFrom";

		// Token: 0x0400014F RID: 335
		private const string EnvelopeRecipients = "Message.EnvelopeRecipients";

		// Token: 0x04000150 RID: 336
		private const string Auth = "Message.Auth";

		// Token: 0x04000151 RID: 337
		private const string DataClassifications = "Message.DataClassifications";

		// Token: 0x04000152 RID: 338
		private const string Size = "Message.Size";

		// Token: 0x04000153 RID: 339
		private const string SenderIp = "Message.SenderIp";

		// Token: 0x04000154 RID: 340
		private static readonly Dictionary<string, KeyValuePair<string, Type>> knownProperties = MessageProperty.InitializeProperties();
	}
}
