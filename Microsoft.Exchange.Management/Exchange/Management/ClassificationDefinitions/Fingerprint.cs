using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000837 RID: 2103
	[Serializable]
	public sealed class Fingerprint
	{
		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x060048FA RID: 18682 RVA: 0x0012BDD0 File Offset: 0x00129FD0
		// (set) Token: 0x060048FB RID: 18683 RVA: 0x0012BDD8 File Offset: 0x00129FD8
		public string Value { get; private set; }

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x060048FC RID: 18684 RVA: 0x0012BDE1 File Offset: 0x00129FE1
		// (set) Token: 0x060048FD RID: 18685 RVA: 0x0012BDE9 File Offset: 0x00129FE9
		public uint ShingleCount { get; private set; }

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x060048FE RID: 18686 RVA: 0x0012BDF2 File Offset: 0x00129FF2
		// (set) Token: 0x060048FF RID: 18687 RVA: 0x0012BDFA File Offset: 0x00129FFA
		public string Description { get; private set; }

		// Token: 0x06004900 RID: 18688 RVA: 0x0012BE0C File Offset: 0x0012A00C
		public Fingerprint(string fingerprintValue, uint shingleCount, string description)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("fingerprintValue", fingerprintValue);
			ArgumentValidator.ThrowIfInvalidValue<uint>("shingleCount", shingleCount, (uint count) => count > 0U);
			try
			{
				Convert.FromBase64String(fingerprintValue);
			}
			catch (FormatException innerException)
			{
				throw new ErrorInvalidFingerprintException(fingerprintValue, innerException);
			}
			this.Value = fingerprintValue;
			this.ShingleCount = shingleCount;
			this.Description = description;
			this.ActualDescription = description;
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x0012BE90 File Offset: 0x0012A090
		public static Fingerprint Parse(string input)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("input", input);
			string s = string.Empty;
			string description = string.Empty;
			string text = string.Empty;
			int num = input.LastIndexOf(":", input.Length - 1);
			if (num >= 0)
			{
				text = input.Substring(num + 1);
				int num2 = input.LastIndexOf(":", num - 1);
				if (num2 >= 0)
				{
					s = input.Substring(num2 + 1, num - num2 - 1);
					description = input.Substring(0, num2);
				}
				else
				{
					s = input.Substring(0, num);
				}
				uint num3;
				if (!string.IsNullOrEmpty(text) && uint.TryParse(s, out num3) && num3 > 0U)
				{
					return new Fingerprint(text, num3, description);
				}
			}
			throw new ErrorInvalidFingerprintException(input);
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x0012BF40 File Offset: 0x0012A140
		public override string ToString()
		{
			return string.Join(":", new object[]
			{
				this.Description,
				this.ShingleCount,
				this.Value
			});
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x0012BF80 File Offset: 0x0012A180
		internal XElement ToXElement()
		{
			XElement xelement = new XElement(XmlProcessingUtils.GetMceNsQualifiedNodeName("Fingerprint"), new object[]
			{
				new XAttribute("id", this.Identity),
				new XAttribute("shingleCount", this.ShingleCount),
				new XAttribute("threshold", 50),
				this.Value
			});
			xelement.SetAttributeValue("description", this.Description);
			return xelement;
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x0012C014 File Offset: 0x0012A214
		internal static Fingerprint FromXElement(XElement element)
		{
			ArgumentValidator.ThrowIfNull("element", element);
			if (!"Fingerprint".Equals(element.Name.LocalName, StringComparison.Ordinal))
			{
				throw new ErrorInvalidFingerprintException(element.ToString());
			}
			string attributeValue = XmlProcessingUtils.GetAttributeValue(element, "id");
			uint num = 0U;
			uint.TryParse(XmlProcessingUtils.GetAttributeValue(element, "shingleCount"), out num);
			string value = element.Value;
			string attributeValue2 = XmlProcessingUtils.GetAttributeValue(element, "description");
			if (num <= 0U || string.IsNullOrEmpty(attributeValue) || string.IsNullOrEmpty(value))
			{
				throw new ErrorInvalidFingerprintException(element.ToString());
			}
			return new Fingerprint(value, num, attributeValue2)
			{
				Identity = attributeValue
			};
		}

		// Token: 0x04002C28 RID: 11304
		private const string Separator = ":";

		// Token: 0x04002C29 RID: 11305
		internal static readonly Fingerprint.FingerprintComparer Comparer = new Fingerprint.FingerprintComparer();

		// Token: 0x04002C2A RID: 11306
		[NonSerialized]
		internal string ActualDescription;

		// Token: 0x04002C2B RID: 11307
		[NonSerialized]
		internal string Identity;

		// Token: 0x02000838 RID: 2104
		internal sealed class FingerprintComparer : EqualityComparer<Fingerprint>
		{
			// Token: 0x06004907 RID: 18695 RVA: 0x0012C0C4 File Offset: 0x0012A2C4
			public override bool Equals(Fingerprint left, Fingerprint right)
			{
				if (!object.ReferenceEquals(left, right))
				{
					if (left == null || right == null)
					{
						return false;
					}
					if (left.ShingleCount != right.ShingleCount)
					{
						return false;
					}
					if (!string.Equals(left.Value, right.Value, StringComparison.Ordinal))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06004908 RID: 18696 RVA: 0x0012C100 File Offset: 0x0012A300
			public override int GetHashCode(Fingerprint fingerprint)
			{
				if (fingerprint != null)
				{
					return string.Join(":", new object[]
					{
						fingerprint.ShingleCount,
						fingerprint.Value
					}).GetHashCode();
				}
				return 0;
			}
		}
	}
}
