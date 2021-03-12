using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000223 RID: 547
	internal class MessageProperty : Property
	{
		// Token: 0x060014DA RID: 5338 RVA: 0x00049FEE File Offset: 0x000481EE
		public MessageProperty(string propertyName, Type type) : base(propertyName, type)
		{
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00049FF8 File Offset: 0x000481F8
		public static MessageProperty Create(string propertyName)
		{
			KeyValuePair<string, Type> keyValuePair;
			if (MessageProperty.knownProperties.TryGetValue(propertyName, out keyValuePair))
			{
				return new MessageProperty(keyValuePair.Key, keyValuePair.Value);
			}
			return new MessageProperty(propertyName, typeof(string));
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0004A038 File Offset: 0x00048238
		protected override object OnGetValue(RulesEvaluationContext baseContext)
		{
			OwaRulesEvaluationContext owaRulesEvaluationContext = (OwaRulesEvaluationContext)baseContext;
			object result = null;
			string name;
			if ((name = base.Name) != null)
			{
				if (!(name == "Message.From"))
				{
					if (!(name == "Message.To"))
					{
						if (name == "Message.DataClassifications")
						{
							result = owaRulesEvaluationContext.DataClassifications;
						}
					}
					else
					{
						ShortList<string> recipients = owaRulesEvaluationContext.Recipients;
						if (recipients != null && recipients.Any<string>())
						{
							result = recipients;
						}
					}
				}
				else
				{
					result = new ShortList<string>
					{
						owaRulesEvaluationContext.FromAddress
					};
				}
			}
			return result;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0004A0BC File Offset: 0x000482BC
		private static Dictionary<string, KeyValuePair<string, Type>> InitializeProperties()
		{
			Dictionary<string, KeyValuePair<string, Type>> dictionary = new Dictionary<string, KeyValuePair<string, Type>>(3);
			MessageProperty.AddProperty("Message.From", typeof(ShortList<string>), dictionary);
			MessageProperty.AddProperty("Message.To", typeof(ShortList<string>), dictionary);
			MessageProperty.AddProperty("Message.DataClassifications", typeof(IEnumerable<DiscoveredDataClassification>), dictionary);
			return dictionary;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0004A110 File Offset: 0x00048310
		private static void AddProperty(string name, Type type, Dictionary<string, KeyValuePair<string, Type>> properties)
		{
			properties.Add(name, new KeyValuePair<string, Type>(name, type));
		}

		// Token: 0x04000B4D RID: 2893
		public const string From = "Message.From";

		// Token: 0x04000B4E RID: 2894
		public const string To = "Message.To";

		// Token: 0x04000B4F RID: 2895
		public const string DataClassifications = "Message.DataClassifications";

		// Token: 0x04000B50 RID: 2896
		private static readonly Dictionary<string, KeyValuePair<string, Type>> knownProperties = MessageProperty.InitializeProperties();
	}
}
