using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000183 RID: 387
	internal class SmtpDomainWithSubdomainsListAsStringCoverter : ICustomTextConverter, ICustomFormatter
	{
		// Token: 0x06000F3D RID: 3901 RVA: 0x0003AFA0 File Offset: 0x000391A0
		object ICustomTextConverter.Parse(Type deservedType, string s, IFormatProvider provider)
		{
			if (deservedType != typeof(MultiValuedProperty<SmtpDomainWithSubdomains>))
			{
				throw new NotSupportedException();
			}
			if (string.IsNullOrEmpty(s))
			{
				throw new FormatException(Strings.ValueCanNotBeEmpty);
			}
			MultiValuedProperty<SmtpDomainWithSubdomains> multiValuedProperty = new MultiValuedProperty<SmtpDomainWithSubdomains>();
			foreach (string text in s.Split(new char[]
			{
				','
			}))
			{
				if (!string.IsNullOrEmpty(text))
				{
					SmtpDomainWithSubdomains smtpDomainWithSubdomains = SmtpDomainWithSubdomains.Parse(text);
					if (SmtpDomainWithSubdomains.StarDomain.Equals(smtpDomainWithSubdomains))
					{
						throw new FormatException(Strings.DisallowStarDomainConstraintText);
					}
					if (!multiValuedProperty.Contains(smtpDomainWithSubdomains))
					{
						multiValuedProperty.Add(smtpDomainWithSubdomains);
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0003B050 File Offset: 0x00039250
		string ICustomFormatter.Format(string format, object arg, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			MultiValuedProperty<SmtpDomainWithSubdomains> multiValuedProperty = arg as MultiValuedProperty<SmtpDomainWithSubdomains>;
			if (multiValuedProperty != null)
			{
				foreach (SmtpDomainWithSubdomains smtpDomainWithSubdomains in multiValuedProperty)
				{
					stringBuilder.Append(','.ToString());
					stringBuilder.Append(smtpDomainWithSubdomains.ToString());
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(0, 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000605 RID: 1541
		public const char SeperatorSign = ',';
	}
}
