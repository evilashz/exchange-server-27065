using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000594 RID: 1428
	[XmlType("MPA")]
	[Serializable]
	public sealed class MailboxProvisioningAttribute : XMLSerializableBase
	{
		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x0600428C RID: 17036 RVA: 0x000FAD42 File Offset: 0x000F8F42
		// (set) Token: 0x0600428D RID: 17037 RVA: 0x000FAD4A File Offset: 0x000F8F4A
		[XmlAttribute(AttributeName = "K")]
		public string Key { get; set; }

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x0600428E RID: 17038 RVA: 0x000FAD53 File Offset: 0x000F8F53
		// (set) Token: 0x0600428F RID: 17039 RVA: 0x000FAD5B File Offset: 0x000F8F5B
		[XmlAttribute(AttributeName = "V")]
		public string Value { get; set; }

		// Token: 0x06004290 RID: 17040 RVA: 0x000FAD64 File Offset: 0x000F8F64
		public override string ToString()
		{
			return string.Format("{0}={1}", this.Key, this.Value);
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x000FAD7C File Offset: 0x000F8F7C
		public static MailboxProvisioningAttribute Parse(string attribute)
		{
			if (string.IsNullOrWhiteSpace(attribute))
			{
				throw new InvalidMailboxProvisioningAttributeException(DirectoryStrings.ErrorInvalidMailboxProvisioningAttribute(""));
			}
			string[] array = attribute.Split(new char[]
			{
				'='
			});
			if (array.Length != 2)
			{
				throw new InvalidMailboxProvisioningAttributeException(DirectoryStrings.ErrorInvalidMailboxProvisioningAttribute(attribute));
			}
			if (!MailboxProvisioningAttribute.RegexConstraintValidation.IsMatch(array[0]) || !MailboxProvisioningAttribute.RegexConstraintValidation.IsMatch(array[1]))
			{
				throw new InvalidMailboxProvisioningAttributeException(DirectoryStrings.ErrorInvalidMailboxProvisioningAttribute(attribute));
			}
			return new MailboxProvisioningAttribute
			{
				Key = array[0],
				Value = array[1]
			};
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x000FAE0A File Offset: 0x000F900A
		private bool Equals(MailboxProvisioningAttribute other)
		{
			return string.Equals(this.Key, other.Key) && string.Equals(this.Value, other.Value);
		}

		// Token: 0x04002D48 RID: 11592
		private static readonly Regex RegexConstraintValidation = new Regex("^[a-z0-9]{1,128}$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
