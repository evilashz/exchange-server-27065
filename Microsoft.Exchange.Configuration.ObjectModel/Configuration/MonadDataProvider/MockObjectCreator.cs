using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001A6 RID: 422
	internal abstract class MockObjectCreator
	{
		// Token: 0x06000F74 RID: 3956
		internal abstract IList<string> GetProperties(string fullName);

		// Token: 0x06000F75 RID: 3957
		protected abstract void FillProperties(Type type, PSObject psObject, object dummyObject, IList<string> properties);

		// Token: 0x06000F76 RID: 3958 RVA: 0x0002D62A File Offset: 0x0002B82A
		internal virtual object CreateDummyObject(Type type)
		{
			return Activator.CreateInstance(type, true);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0002D634 File Offset: 0x0002B834
		internal virtual object TranslateToMockObject(Type type, PSObject psObject)
		{
			object obj = this.CreateDummyObject(type);
			IList<string> properties = this.GetProperties(type.FullName);
			this.FillProperties(type, psObject, obj, properties);
			return obj;
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0002D664 File Offset: 0x0002B864
		protected static object GetPropertyBasedOnDefinition(PropertyDefinition definition, object propertyValue)
		{
			if (!(propertyValue is PSObject) || !(((PSObject)propertyValue).BaseObject is IList))
			{
				return MockObjectCreator.GetSingleProperty(propertyValue, definition.Type);
			}
			IList list = ((PSObject)propertyValue).BaseObject as IList;
			if (definition.Type == typeof(ScheduleInterval[]))
			{
				List<ScheduleInterval> list2 = new List<ScheduleInterval>();
				foreach (object prop in list)
				{
					list2.Add((ScheduleInterval)MockObjectCreator.GetSingleProperty(prop, typeof(ScheduleInterval)));
				}
				return list2.ToArray();
			}
			MultiValuedPropertyBase multiValuedPropertyBase = null;
			if (definition.Type == typeof(ProxyAddress))
			{
				multiValuedPropertyBase = new ProxyAddressCollection();
			}
			else if (definition.Type == typeof(ApprovedApplicationCollection))
			{
				multiValuedPropertyBase = new ApprovedApplicationCollection();
			}
			else
			{
				if (definition.Type.FullName == "Microsoft.Exchange.Management.RecipientTasks.MailboxRights[]")
				{
					if (MockObjectCreator.mailboxRightsEnum == null)
					{
						string assemblyFile = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Management.Recipient.dll");
						MockObjectCreator.mailboxRightsEnum = Assembly.LoadFrom(assemblyFile).GetType("Microsoft.Exchange.Management.RecipientTasks.MailboxRights");
					}
					Array array = Array.CreateInstance(MockObjectCreator.mailboxRightsEnum, list.Count);
					int num = 0;
					foreach (object prop2 in list)
					{
						array.SetValue(MockObjectCreator.GetSingleProperty(prop2, MockObjectCreator.mailboxRightsEnum), num);
						num++;
					}
					return array;
				}
				multiValuedPropertyBase = new MultiValuedProperty<object>();
			}
			foreach (object prop3 in list)
			{
				multiValuedPropertyBase.Add(MockObjectCreator.GetSingleProperty(prop3, definition.Type));
			}
			return multiValuedPropertyBase;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0002D890 File Offset: 0x0002BA90
		protected static object GetSingleProperty(object prop, Type type)
		{
			if (prop == null)
			{
				return null;
			}
			object obj = null;
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				obj = MockObjectCreator.GetSingleProperty(prop, type.GetGenericArguments()[0]);
			}
			else if (type == typeof(ADObjectId) && prop is PSObject)
			{
				obj = new ADObjectId(((PSObject)prop).Members["DistinguishedName"].Value.ToString(), new Guid(((PSObject)prop).Members["ObjectGuid"].Value.ToString()));
			}
			else if (type == typeof(EnhancedTimeSpan))
			{
				obj = EnhancedTimeSpan.Parse(prop.ToString());
			}
			else if (type == typeof(Unlimited<EnhancedTimeSpan>))
			{
				obj = Unlimited<EnhancedTimeSpan>.Parse(prop.ToString());
			}
			else if (type == typeof(ByteQuantifiedSize))
			{
				obj = ByteQuantifiedSize.Parse(prop.ToString());
			}
			else if (type == typeof(Unlimited<ByteQuantifiedSize>))
			{
				obj = Unlimited<ByteQuantifiedSize>.Parse(prop.ToString());
			}
			else if (type == typeof(Unlimited<int>))
			{
				obj = Unlimited<int>.Parse(prop.ToString());
			}
			else if (type == typeof(ProxyAddress))
			{
				obj = ProxyAddress.Parse(prop.ToString());
			}
			else if (type == typeof(SmtpAddress))
			{
				obj = new SmtpAddress(prop.ToString());
			}
			else if (type == typeof(SmtpDomain))
			{
				obj = SmtpDomain.Parse(prop.ToString());
			}
			else if (type == typeof(CountryInfo))
			{
				obj = CountryInfo.Parse(prop.ToString());
			}
			else if (type == typeof(SharingPolicyDomain))
			{
				obj = SharingPolicyDomain.Parse(prop.ToString());
			}
			else if (type == typeof(ApprovedApplication))
			{
				obj = ApprovedApplication.Parse(prop.ToString());
			}
			else if (type == typeof(SmtpDomainWithSubdomains))
			{
				obj = SmtpDomainWithSubdomains.Parse(prop.ToString());
			}
			else if (type == typeof(UMLanguage))
			{
				obj = UMLanguage.Parse(prop.ToString());
			}
			else if (type == typeof(UMSmartHost))
			{
				obj = UMSmartHost.Parse(prop.ToString());
			}
			else if (type == typeof(ScheduleInterval))
			{
				obj = ScheduleInterval.Parse(prop.ToString());
			}
			else if (type == typeof(NumberFormat))
			{
				obj = NumberFormat.Parse(prop.ToString());
			}
			else if (type == typeof(DialGroupEntry))
			{
				obj = DialGroupEntry.Parse(prop.ToString());
			}
			else if (type == typeof(CustomMenuKeyMapping))
			{
				obj = CustomMenuKeyMapping.Parse(prop.ToString());
			}
			else if (type == typeof(HolidaySchedule))
			{
				obj = HolidaySchedule.Parse(prop.ToString());
			}
			else if (type == typeof(UMTimeZone))
			{
				obj = UMTimeZone.Parse(prop.ToString());
			}
			else if (type == typeof(ServerVersion))
			{
				obj = ServerVersion.ParseFromSerialNumber(prop.ToString());
			}
			else if (type == typeof(X509Certificate2))
			{
				obj = new X509Certificate2(((PSObject)prop).Members["RawData"].Value as byte[]);
			}
			else if (type == typeof(LocalizedString))
			{
				obj = new LocalizedString(prop.ToString());
			}
			else if (type == typeof(ExchangeObjectVersion))
			{
				obj = ExchangeObjectVersion.Parse(prop.ToString());
			}
			else if (type == typeof(bool))
			{
				obj = bool.Parse(prop.ToString());
			}
			else if (type == typeof(SecurityPrincipalIdParameter))
			{
				obj = new SecurityPrincipalIdParameter(prop.ToString());
			}
			else if (type == typeof(ActiveDirectoryAccessRule))
			{
				obj = (prop as ActiveDirectoryAccessRule);
			}
			else if (type == typeof(ObjectId))
			{
				string text = prop.ToString();
				if (!ADObjectId.IsValidDistinguishedName(text) && text.Contains("/"))
				{
					text = MockObjectCreator.ConvertDNFromTreeStructure(text);
				}
				obj = new ADObjectId(text);
			}
			else if (type.IsEnum)
			{
				try
				{
					obj = Enum.Parse(type, prop.ToString());
				}
				catch (ArgumentException)
				{
					obj = Enum.GetValues(type).GetValue(0);
				}
			}
			return obj ?? prop;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0002DDAC File Offset: 0x0002BFAC
		private static string ConvertDNFromTreeStructure(string treeDN)
		{
			if (string.IsNullOrEmpty(treeDN))
			{
				throw new ArgumentException("The input param of tree structure identity should not be null.");
			}
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = treeDN.Split(new char[]
			{
				'/'
			});
			if (array.Length > 2)
			{
				string text = array[1];
				if (text == "Domain Controllers" || text == "Microsoft Exchange Security Groups")
				{
					MockObjectCreator.BuildCNPartOfDN(array, stringBuilder, array.Length - 1, 1);
					MockObjectCreator.BuildOUPartOfDN(text, stringBuilder);
				}
				else if (text == "Microsoft Exchange Hosted Organizations")
				{
					MockObjectCreator.BuildCNPartOfDN(array, stringBuilder, array.Length - 1, 2);
					MockObjectCreator.BuildOUPartOfDN(array[2], stringBuilder);
					MockObjectCreator.BuildOUPartOfDN(text, stringBuilder);
				}
				else
				{
					MockObjectCreator.BuildCNPartOfDN(array, stringBuilder, array.Length - 1, 0);
				}
				MockObjectCreator.BuildDCPartOfDN(array[0].Split(new char[]
				{
					'.'
				}), stringBuilder);
				return stringBuilder.ToString().TrimEnd(new char[]
				{
					','
				});
			}
			return treeDN;
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0002DE9C File Offset: 0x0002C09C
		private static void BuildCNPartOfDN(string[] treeItems, StringBuilder dnBuilder, int toIndex, int fromIndex)
		{
			for (int i = toIndex; i > fromIndex; i--)
			{
				dnBuilder.Append("CN=");
				dnBuilder.Append(treeItems[i]);
				dnBuilder.Append(',');
			}
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0002DED4 File Offset: 0x0002C0D4
		private static void BuildDCPartOfDN(string[] treeItems, StringBuilder dnBuilder)
		{
			foreach (string value in treeItems)
			{
				dnBuilder.Append("DC=");
				dnBuilder.Append(value);
				dnBuilder.Append(',');
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0002DF12 File Offset: 0x0002C112
		private static void BuildOUPartOfDN(string ouPart, StringBuilder dnBuilder)
		{
			dnBuilder.Append("OU=");
			dnBuilder.Append(ouPart);
			dnBuilder.Append(',');
		}

		// Token: 0x04000345 RID: 837
		private const string PrefixForCN = "CN=";

		// Token: 0x04000346 RID: 838
		private const string PrefixForDC = "DC=";

		// Token: 0x04000347 RID: 839
		private const string PrefixForOU = "OU=";

		// Token: 0x04000348 RID: 840
		private const string DomainControllers = "Domain Controllers";

		// Token: 0x04000349 RID: 841
		private const string SecurityGroups = "Microsoft Exchange Security Groups";

		// Token: 0x0400034A RID: 842
		private const string HostedOrganizations = "Microsoft Exchange Hosted Organizations";

		// Token: 0x0400034B RID: 843
		private static Type mailboxRightsEnum;
	}
}
