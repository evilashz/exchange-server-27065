using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.ReportingTask;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x020003C4 RID: 964
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	internal sealed class CmdletValidator : Attribute
	{
		// Token: 0x060022B2 RID: 8882 RVA: 0x0008D310 File Offset: 0x0008B510
		static CmdletValidator()
		{
			CmdletValidator.validationFunctions.Add("ValidateDlpPolicy", delegate(CmdletValidator validator, CmdletValidator.CmdletValidatorArgs args)
			{
				validator.ValidateDlpPolicy(args.Property, args.Task, args.ConfigSession);
			});
			CmdletValidator.validationFunctions.Add("ValidateDomain", delegate(CmdletValidator validator, CmdletValidator.CmdletValidatorArgs args)
			{
				validator.ValidateDomain(args.Property, args.Task, args.ConfigSession);
			});
			CmdletValidator.validationFunctions.Add("ValidateOrganization", delegate(CmdletValidator validator, CmdletValidator.CmdletValidatorArgs args)
			{
				validator.ValidateOrganization(args.Property, args.Task);
			});
			CmdletValidator.validationFunctions.Add("ValidateRequiredField", delegate(CmdletValidator validator, CmdletValidator.CmdletValidatorArgs args)
			{
				validator.ValidateRequiredField(args.Property, args.Task);
			});
			CmdletValidator.validationFunctions.Add("ValidateTransportRule", delegate(CmdletValidator validator, CmdletValidator.CmdletValidatorArgs args)
			{
				validator.ValidateTransportRule(args.Property, args.Task, args.ConfigSession);
			});
			CmdletValidator.validationFunctions.Add("ScrubDlp", delegate(CmdletValidator validator, CmdletValidator.CmdletValidatorArgs args)
			{
				validator.ScrubDlp(args.Property, args.Task, (Schema.EventTypes)validator.parameters[0]);
			});
			CmdletValidator.validationFunctions.Add("ValidateIntRange", delegate(CmdletValidator validator, CmdletValidator.CmdletValidatorArgs args)
			{
				validator.ValidateIntRange(args.Property, args.Task, (int)validator.parameters[0], (int)validator.parameters[1]);
			});
			CmdletValidator.validationFunctions.Add("ValidateEmailAddress", delegate(CmdletValidator validator, CmdletValidator.CmdletValidatorArgs args)
			{
				validator.ValidateEmailAddress(args.Property, args.Task, null, (CmdletValidator.EmailAddress)validator.parameters[0], (CmdletValidator.WildcardValidationOptions)validator.parameters[1], CmdletValidator.EmailAcceptedDomainOptions.SkipVerify);
			});
			CmdletValidator.validationFunctions.Add("ValidateEmailAddressWithDomain", delegate(CmdletValidator validator, CmdletValidator.CmdletValidatorArgs args)
			{
				validator.ValidateEmailAddress(args.Property, args.Task, args.ConfigSession, (CmdletValidator.EmailAddress)validator.parameters[0], (CmdletValidator.WildcardValidationOptions)validator.parameters[1], (CmdletValidator.EmailAcceptedDomainOptions)validator.parameters[2]);
			});
			CmdletValidator.validationFunctions.Add("ValidateEnum", delegate(CmdletValidator validator, CmdletValidator.CmdletValidatorArgs args)
			{
				validator.ValidateEnum(args.Property, args.Task, (Type)validator.parameters[0], (validator.parameters.Length > 1) ? Convert.ToUInt64(validator.parameters[1]) : ulong.MaxValue);
			});
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0008D4DF File Offset: 0x0008B6DF
		internal CmdletValidator(string method, params object[] parameters)
		{
			this.methodName = method;
			this.parameters = parameters;
			this.ValidatorType = CmdletValidator.ValidatorTypes.Preprocessing;
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x0008D4FC File Offset: 0x0008B6FC
		// (set) Token: 0x060022B5 RID: 8885 RVA: 0x0008D504 File Offset: 0x0008B704
		public CmdletValidator.ValidatorTypes ValidatorType { get; set; }

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x0008D50D File Offset: 0x0008B70D
		// (set) Token: 0x060022B7 RID: 8887 RVA: 0x0008D532 File Offset: 0x0008B732
		public Strings.IDs ErrorMessage
		{
			get
			{
				if (this.errorMessageId != null)
				{
					return this.errorMessageId.Value;
				}
				throw new InvalidOperationException("ErrorMessage must be set before accessing it.");
			}
			set
			{
				this.errorMessageId = new Strings.IDs?(value);
			}
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0008D540 File Offset: 0x0008B740
		internal void Validate(CmdletValidator.CmdletValidatorArgs args)
		{
			CmdletValidator.ValidationDelegate validationDelegate = CmdletValidator.validationFunctions[this.methodName];
			validationDelegate(this, args);
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0008D568 File Offset: 0x0008B768
		internal void ValidateOrganization(PropertyInfo property, object task)
		{
			if (!(property.GetValue(task, null) is OrganizationIdParameter))
			{
				Type type = task.GetType();
				OrganizationId organizationId = type.GetProperty("CurrentOrganizationId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(task, null) as OrganizationId;
				if (organizationId == null || organizationId.OrganizationalUnit == null)
				{
					this.ThrowError(property);
				}
				OrganizationIdParameter value = new OrganizationIdParameter(organizationId.OrganizationalUnit.Name);
				property.SetValue(task, value, null);
			}
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0008D5E4 File Offset: 0x0008B7E4
		internal void ValidateEnum(PropertyInfo property, object task, Type enumType, ulong enumerationSubset)
		{
			IList<string> list;
			if (this.TryGetValues<string>(property, task, out list))
			{
				int num;
				if (list.Count == 1 && int.TryParse(list[0], out num))
				{
					this.ThrowError(property);
				}
				string value = string.Join(",", list);
				try
				{
					if (!string.IsNullOrEmpty(value))
					{
						ulong num2 = Convert.ToUInt64(Enum.Parse(enumType, value, true));
						ulong num3 = ~enumerationSubset;
						if ((num3 & num2) != 0UL)
						{
							this.ThrowError(property);
						}
					}
					return;
				}
				catch (ArgumentException)
				{
					this.ThrowError(property);
					return;
				}
			}
			ParameterAttribute[] array = (ParameterAttribute[])property.GetCustomAttributes(typeof(ParameterAttribute), false);
			if (array != null)
			{
				if (!array.Any((ParameterAttribute a) => a.Mandatory) && array.Count<ParameterAttribute>() != 0)
				{
					return;
				}
			}
			this.ThrowError(property);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0008D6C4 File Offset: 0x0008B8C4
		internal void ValidateIntRange(PropertyInfo property, object task, int min, int max)
		{
			int num = (int)property.GetValue(task, null);
			if (min > num || max < num)
			{
				this.ThrowError(property);
			}
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0008D704 File Offset: 0x0008B904
		internal void ValidateEmailAddress(PropertyInfo property, object task, IConfigDataProvider configSession, CmdletValidator.EmailAddress addressType, CmdletValidator.WildcardValidationOptions wildcardOptions, CmdletValidator.EmailAcceptedDomainOptions domainOptions = CmdletValidator.EmailAcceptedDomainOptions.SkipVerify)
		{
			IList<string> list;
			if (this.TryGetValues<string>(property, task, out list))
			{
				LocalizedString message = (addressType == CmdletValidator.EmailAddress.Sender) ? Strings.InvalidSenderAddress : Strings.InvalidRecipientAddress;
				IEnumerable<string> source = new string[0];
				if (domainOptions == CmdletValidator.EmailAcceptedDomainOptions.Verify)
				{
					AcceptedDomainIdParameter acceptedDomainIdParameter = AcceptedDomainIdParameter.Parse("*");
					source = from domain in acceptedDomainIdParameter.GetObjects<AcceptedDomain>(null, configSession)
					select domain.DomainName.Domain.ToLower();
				}
				foreach (string address in list)
				{
					bool flag2;
					bool flag = Schema.Utilities.ValidateEmailAddress(address, out flag2);
					if (flag && flag2)
					{
						flag &= (wildcardOptions == CmdletValidator.WildcardValidationOptions.Allow);
						if (flag && list.Count > 1)
						{
							message = ((addressType == CmdletValidator.EmailAddress.Sender) ? Strings.CannotCombineWildcardSenderAddress : Strings.CannotCombineWildcardRecipientAddress);
							flag = false;
						}
					}
					if (flag && domainOptions == CmdletValidator.EmailAcceptedDomainOptions.Verify)
					{
						SmtpAddress smtpAddress = new SmtpAddress(address);
						if (!source.Contains(smtpAddress.Domain, StringComparer.InvariantCultureIgnoreCase))
						{
							flag = false;
							message = Strings.EmailAddressNotInAcceptedDomain(address);
						}
					}
					if (!flag)
					{
						if (this.errorMessageId == null)
						{
							throw new InvalidExpressionException(message);
						}
						this.ThrowError(property);
					}
				}
			}
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0008D844 File Offset: 0x0008BA44
		internal void ScrubDlp(PropertyInfo property, object task, Schema.EventTypes eventTypes)
		{
			Task task2 = task as Task;
			IList<string> collection;
			if ((task2 == null || !Schema.Utilities.HasDlpRole(task2)) && this.TryGetValues<string>(property, task, out collection))
			{
				List<string> list = new List<string>(collection);
				if (list.Count == 0)
				{
					list.AddRange(Schema.Utilities.Split(eventTypes));
				}
				else
				{
					Schema.Utilities.RemoveDlpEventTypes(list);
				}
				if (list.Count == 0)
				{
					this.ThrowError(property);
				}
				property.SetValue(task, new MultiValuedProperty<string>(list), null);
			}
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x0008D8B4 File Offset: 0x0008BAB4
		internal void ValidateRequiredField(PropertyInfo property, object task)
		{
			object value = property.GetValue(task, null);
			if (value is IList)
			{
				IList list = (IList)value;
				if (list.Count > 0)
				{
					return;
				}
			}
			else if (property.PropertyType.IsValueType)
			{
				object obj = Activator.CreateInstance(property.PropertyType);
				if (!value.Equals(obj))
				{
					return;
				}
			}
			else if (value != null)
			{
				return;
			}
			throw new InvalidExpressionException(Strings.RequiredReportingParameter(property.Name));
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0008D94C File Offset: 0x0008BB4C
		internal void ValidateDomain(PropertyInfo property, object task, IConfigDataProvider configSession)
		{
			IList<Fqdn> list;
			if (this.TryGetValues<Fqdn>(property, task, out list) && list.Count > 0)
			{
				if (configSession == null)
				{
					throw new NullReferenceException("ValidateDomain requires an IConfigDataProvider");
				}
				AcceptedDomainIdParameter acceptedDomainIdParameter = AcceptedDomainIdParameter.Parse("*");
				HashSet<string> acceptedDomains = new HashSet<string>(from domain in acceptedDomainIdParameter.GetObjects<AcceptedDomain>(null, configSession)
				select domain.DomainName.Domain.ToLower());
				if (!list.All((Fqdn domain) => acceptedDomains.Contains(domain.Domain.ToLower())))
				{
					this.ThrowError(property);
				}
			}
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x0008DA08 File Offset: 0x0008BC08
		internal void ValidateDlpPolicy(PropertyInfo property, object task, IConfigDataProvider configSession)
		{
			IList<string> list;
			if (!DatacenterRegistry.IsForefrontForOffice() && this.TryGetValues<string>(property, task, out list) && list.Count > 0)
			{
				if (configSession == null)
				{
					throw new NullReferenceException("ValidateDlpPolicy requires an IConfigDataProvider");
				}
				HashSet<string> installedDlp = new HashSet<string>(from dlp in DlpUtils.GetInstalledTenantDlpPolicies(configSession)
				select dlp.Name.ToLower());
				if (!list.All((string dlp) => installedDlp.Contains(dlp.ToLower())))
				{
					this.ThrowError(property);
				}
			}
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0008DAC0 File Offset: 0x0008BCC0
		internal void ValidateTransportRule(PropertyInfo property, object task, IConfigDataProvider configSession)
		{
			IList<string> list;
			if (this.TryGetValues<string>(property, task, out list) && list.Count > 0)
			{
				if (configSession == null)
				{
					throw new NullReferenceException("ValidateTransportRule requires an IConfigDataProvider");
				}
				HashSet<string> installedRules = new HashSet<string>(from rule in DlpUtils.GetTransportRules(configSession, (Rule rule) => true)
				select rule.Name.ToLower());
				if (!list.All((string rule) => installedRules.Contains(rule.ToLower())))
				{
					this.ThrowError(property);
				}
			}
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0008DB64 File Offset: 0x0008BD64
		private void ThrowError(PropertyInfo property)
		{
			if (this.errorMessageId != null)
			{
				throw new InvalidExpressionException(Strings.GetLocalizedString(this.errorMessageId.Value));
			}
			throw new NullReferenceException(string.Format("The error message is not defined for the property {0}.", property.Name));
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0008DBA0 File Offset: 0x0008BDA0
		private bool TryGetValues<TType>(PropertyInfo property, object task, out IList<TType> values)
		{
			object value = property.GetValue(task, null);
			if (value is IList<TType>)
			{
				values = (IList<TType>)value;
			}
			else
			{
				if (value == null)
				{
					values = null;
					return false;
				}
				values = new List<TType>
				{
					(TType)((object)value)
				};
			}
			return true;
		}

		// Token: 0x04001B47 RID: 6983
		private static readonly Dictionary<string, CmdletValidator.ValidationDelegate> validationFunctions = new Dictionary<string, CmdletValidator.ValidationDelegate>();

		// Token: 0x04001B48 RID: 6984
		private readonly string methodName;

		// Token: 0x04001B49 RID: 6985
		private object[] parameters;

		// Token: 0x04001B4A RID: 6986
		private Strings.IDs? errorMessageId;

		// Token: 0x020003C5 RID: 965
		internal static class Methods
		{
			// Token: 0x04001B5C RID: 7004
			internal const string DlpPolicy = "ValidateDlpPolicy";

			// Token: 0x04001B5D RID: 7005
			internal const string Domain = "ValidateDomain";

			// Token: 0x04001B5E RID: 7006
			internal const string EmailAddress = "ValidateEmailAddress";

			// Token: 0x04001B5F RID: 7007
			internal const string EmailAddressWithDomain = "ValidateEmailAddressWithDomain";

			// Token: 0x04001B60 RID: 7008
			internal const string Enum = "ValidateEnum";

			// Token: 0x04001B61 RID: 7009
			internal const string IntRange = "ValidateIntRange";

			// Token: 0x04001B62 RID: 7010
			internal const string Organization = "ValidateOrganization";

			// Token: 0x04001B63 RID: 7011
			internal const string Required = "ValidateRequiredField";

			// Token: 0x04001B64 RID: 7012
			internal const string ScrubDlp = "ScrubDlp";

			// Token: 0x04001B65 RID: 7013
			internal const string TransportRule = "ValidateTransportRule";
		}

		// Token: 0x020003C6 RID: 966
		internal enum ValidatorTypes
		{
			// Token: 0x04001B67 RID: 7015
			Preprocessing,
			// Token: 0x04001B68 RID: 7016
			Postprocessing,
			// Token: 0x04001B69 RID: 7017
			PostprocessingWithConfigSession
		}

		// Token: 0x020003C7 RID: 967
		internal enum EmailAddress
		{
			// Token: 0x04001B6B RID: 7019
			Sender,
			// Token: 0x04001B6C RID: 7020
			Recipient
		}

		// Token: 0x020003C8 RID: 968
		internal enum WildcardValidationOptions
		{
			// Token: 0x04001B6E RID: 7022
			Allow,
			// Token: 0x04001B6F RID: 7023
			Disallow
		}

		// Token: 0x020003C9 RID: 969
		internal enum EmailAcceptedDomainOptions
		{
			// Token: 0x04001B71 RID: 7025
			Verify,
			// Token: 0x04001B72 RID: 7026
			SkipVerify
		}

		// Token: 0x020003CA RID: 970
		// (Invoke) Token: 0x060022D5 RID: 8917
		private delegate void ValidationDelegate(CmdletValidator instance, CmdletValidator.CmdletValidatorArgs args);

		// Token: 0x020003CB RID: 971
		internal class CmdletValidatorArgs
		{
			// Token: 0x060022D8 RID: 8920 RVA: 0x0008DBE6 File Offset: 0x0008BDE6
			internal CmdletValidatorArgs(PropertyInfo property, object task, Func<IConfigDataProvider> configDataProviderFunction = null)
			{
				this.Property = property;
				this.Task = task;
				this.configDataProviderFunction = configDataProviderFunction;
			}

			// Token: 0x17000A52 RID: 2642
			// (get) Token: 0x060022D9 RID: 8921 RVA: 0x0008DC03 File Offset: 0x0008BE03
			// (set) Token: 0x060022DA RID: 8922 RVA: 0x0008DC0B File Offset: 0x0008BE0B
			internal PropertyInfo Property { get; private set; }

			// Token: 0x17000A53 RID: 2643
			// (get) Token: 0x060022DB RID: 8923 RVA: 0x0008DC14 File Offset: 0x0008BE14
			// (set) Token: 0x060022DC RID: 8924 RVA: 0x0008DC1C File Offset: 0x0008BE1C
			internal object Task { get; private set; }

			// Token: 0x17000A54 RID: 2644
			// (get) Token: 0x060022DD RID: 8925 RVA: 0x0008DC25 File Offset: 0x0008BE25
			internal IConfigDataProvider ConfigSession
			{
				get
				{
					if (this.configDataProviderFunction == null)
					{
						return null;
					}
					return this.configDataProviderFunction();
				}
			}

			// Token: 0x04001B73 RID: 7027
			private Func<IConfigDataProvider> configDataProviderFunction;
		}
	}
}
