using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000288 RID: 648
	internal class PswsPropertyConverterModule : TaskIOPipelineBase, ITaskModule
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x00054435 File Offset: 0x00052635
		public bool NeedEncodeDecodeKeyProperties
		{
			get
			{
				return Constants.IsPowerShellWebService && this.context != null && this.context.ExchangeRunspaceConfig != null && this.context.ExchangeRunspaceConfig.ConfigurationSettings.EncodeDecodeKey;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0005446C File Offset: 0x0005266C
		private bool ForceToReturnNonOrgHierarchyIdentity
		{
			get
			{
				return Constants.IsPowerShellWebService && this.context != null && OrganizationId.ForestWideOrgId.Equals(this.context.UserInfo.ExecutingUserOrganizationId) && !OrganizationId.ForestWideOrgId.Equals(this.context.UserInfo.CurrentOrganizationId);
			}
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x000544C3 File Offset: 0x000526C3
		public PswsPropertyConverterModule(TaskContext context)
		{
			this.context = context;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x000544D2 File Offset: 0x000526D2
		public void Init(ITaskEvent task)
		{
			task.PreInit += this.DecodeKeyProperties;
			this.context.CommandShell.PrependTaskIOPipelineHandler(this);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x000544F7 File Offset: 0x000526F7
		public void Dispose()
		{
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x000544FC File Offset: 0x000526FC
		public static bool TryDecodeIIdentityParameter(IIdentityParameter identity, out IIdentityParameter decodedIdentity)
		{
			decodedIdentity = null;
			if (identity == null)
			{
				return false;
			}
			string rawIdentity = identity.RawIdentity;
			if (string.IsNullOrWhiteSpace(rawIdentity))
			{
				return false;
			}
			Type type = identity.GetType();
			string text;
			if (!UrlTokenConverter.TryUrlTokenDecode(rawIdentity, out text))
			{
				return false;
			}
			try
			{
				decodedIdentity = (Activator.CreateInstance(type, new object[]
				{
					text
				}) as IIdentityParameter);
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			catch (MissingMethodException arg)
			{
				throw new InvalidOperationException(string.Format("DEV Bug: The type {0} must have ctor(string) in order to be parsed from string input. Exception: {1}", type, arg));
			}
			return decodedIdentity != null;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00054594 File Offset: 0x00052794
		public static void DecodeKeyProperties(string cmdletName, PropertyBag inputFields)
		{
			List<string> propertiesNeedUrlTokenInputDecode = PswsKeyProperties.GetPropertiesNeedUrlTokenInputDecode(cmdletName);
			foreach (string key in propertiesNeedUrlTokenInputDecode)
			{
				if (inputFields.IsModified(key))
				{
					object obj = inputFields[key];
					IIdentityParameter value;
					if (obj != null && PswsPropertyConverterModule.TryDecodeIIdentityParameter((IIdentityParameter)obj, out value))
					{
						inputFields[key] = value;
					}
				}
			}
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00054610 File Offset: 0x00052810
		public static bool TryConvertOutputObjectKeyProperties(ConvertOutputPropertyEventArgs args, out object convertedValue)
		{
			convertedValue = null;
			ConfigurableObject configurableObject = args.ConfigurableObject;
			PropertyDefinition property = args.Property;
			string propertyInStr = args.PropertyInStr;
			object value = args.Value;
			if (value == null)
			{
				return false;
			}
			if (!PswsKeyProperties.IsKeyProperty(configurableObject, property, propertyInStr))
			{
				return false;
			}
			if (value is string)
			{
				convertedValue = UrlTokenConverter.UrlTokenEncode((string)value);
				return true;
			}
			if (value is ObjectId)
			{
				convertedValue = new UrlTokenEncodedObjectId(value.ToString());
				return true;
			}
			if (value is IUrlTokenEncode)
			{
				((IUrlTokenEncode)value).ReturnUrlTokenEncodedString = true;
				convertedValue = value;
				return true;
			}
			throw new NotSupportedException(string.Format("Value with type {0} is not supported.", value.GetType()));
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000546AC File Offset: 0x000528AC
		private void DecodeKeyProperties(object sender, EventArgs e)
		{
			if (!this.NeedEncodeDecodeKeyProperties)
			{
				return;
			}
			string invocationName = this.context.InvocationInfo.InvocationName;
			PropertyBag fields = this.context.InvocationInfo.Fields;
			PswsPropertyConverterModule.DecodeKeyProperties(invocationName, fields);
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x000546EC File Offset: 0x000528EC
		public override bool WriteObject(object input, out object output)
		{
			ConfigurableObject configurableObject = input as ConfigurableObject;
			if (configurableObject != null)
			{
				if (this.NeedEncodeDecodeKeyProperties)
				{
					configurableObject.OutputPropertyConverter += PswsPropertyConverterModule.TryConvertOutputObjectKeyProperties;
				}
				if (this.ForceToReturnNonOrgHierarchyIdentity)
				{
					NonOrgHierarchyConverter @object = new NonOrgHierarchyConverter(this.context.UserInfo.CurrentOrganizationId);
					configurableObject.OutputPropertyConverter += @object.TryConvertKeyToNonOrgHierarchy;
				}
			}
			output = input;
			return true;
		}

		// Token: 0x040006D5 RID: 1749
		private TaskContext context;
	}
}
