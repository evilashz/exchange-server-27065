using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000684 RID: 1668
	[TypeConverter(typeof(WebServiceReference.WebServiceReferenceTypeConverter))]
	public sealed class WebServiceReference
	{
		// Token: 0x0600481C RID: 18460 RVA: 0x000DB3D4 File Offset: 0x000D95D4
		public WebServiceReference(string serviceUrl)
		{
			this.ServiceUrl = serviceUrl;
			int num = serviceUrl.IndexOf('?');
			if (num >= 0)
			{
				this.serviceAbsolutePath = serviceUrl.Substring(0, num);
				return;
			}
			this.serviceAbsolutePath = serviceUrl;
		}

		// Token: 0x170027A7 RID: 10151
		// (get) Token: 0x0600481D RID: 18461 RVA: 0x000DB411 File Offset: 0x000D9611
		// (set) Token: 0x0600481E RID: 18462 RVA: 0x000DB419 File Offset: 0x000D9619
		public string ServiceUrl { get; private set; }

		// Token: 0x170027A8 RID: 10152
		// (get) Token: 0x0600481F RID: 18463 RVA: 0x000DB424 File Offset: 0x000D9624
		public Type ServiceType
		{
			get
			{
				if (this.serviceType == null && !WebServiceReference.knownServiceTypes.TryGetValue(this.serviceAbsolutePath, out this.serviceType))
				{
					this.serviceType = WebServiceReference.WebServiceReferenceTypeConverter.GetServiceClass(this.serviceAbsolutePath);
					WebServiceReference.knownServiceTypes[this.serviceAbsolutePath] = this.serviceType;
				}
				return this.serviceType;
			}
		}

		// Token: 0x170027A9 RID: 10153
		// (get) Token: 0x06004820 RID: 18464 RVA: 0x000DB484 File Offset: 0x000D9684
		public object ServiceInstance
		{
			get
			{
				if (this.serviceInstance == null)
				{
					this.serviceInstance = Activator.CreateInstance(this.ServiceType);
					MethodInfo method = this.ServiceType.GetMethod("InitializeOperationContext", new Type[]
					{
						typeof(string)
					});
					if (method != null)
					{
						method.Invoke(this.serviceInstance, new object[]
						{
							this.ServiceUrl
						});
					}
				}
				return this.serviceInstance;
			}
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x000DB500 File Offset: 0x000D9700
		public PowerShellResults GetObject(Identity identity)
		{
			MethodInfo method = this.ServiceType.GetMethod("GetObject", new Type[]
			{
				typeof(Identity)
			});
			return (PowerShellResults)method.Invoke(this.ServiceInstance, new object[]
			{
				identity
			});
		}

		// Token: 0x06004822 RID: 18466 RVA: 0x000DB550 File Offset: 0x000D9750
		public PowerShellResults GetObjectForNew(Identity identity)
		{
			MethodInfo method = this.ServiceType.GetMethod("GetObjectForNew", new Type[]
			{
				typeof(Identity)
			});
			return (PowerShellResults)method.Invoke(this.ServiceInstance, new object[]
			{
				identity
			});
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x000DB5A0 File Offset: 0x000D97A0
		public PowerShellResults GetObjectOnDemand(Identity identity, string workflowName)
		{
			MethodInfo method = this.ServiceType.GetMethod("GetObjectOnDemand", new Type[]
			{
				typeof(Identity),
				typeof(string)
			});
			return (PowerShellResults)method.Invoke(this.ServiceInstance, new object[]
			{
				identity,
				workflowName
			});
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x000DB604 File Offset: 0x000D9804
		public PowerShellResults<JsonDictionary<object>> GetList(DDIParameters filter, SortOptions sort)
		{
			MethodInfo method = this.ServiceType.GetMethod("GetList", new Type[]
			{
				typeof(DDIParameters),
				typeof(SortOptions)
			});
			return (PowerShellResults<JsonDictionary<object>>)method.Invoke(this.ServiceInstance, new object[]
			{
				filter,
				sort
			});
		}

		// Token: 0x0400306D RID: 12397
		private static readonly SynchronizedDictionary<string, Type> knownServiceTypes = new SynchronizedDictionary<string, Type>();

		// Token: 0x0400306E RID: 12398
		private string serviceAbsolutePath;

		// Token: 0x0400306F RID: 12399
		private Type serviceType;

		// Token: 0x04003070 RID: 12400
		private object serviceInstance;

		// Token: 0x02000685 RID: 1669
		private class WebServiceReferenceTypeConverter : TypeConverter
		{
			// Token: 0x06004826 RID: 18470 RVA: 0x000DB671 File Offset: 0x000D9871
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06004827 RID: 18471 RVA: 0x000DB690 File Offset: 0x000D9890
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					string serviceUrl = (value as string).Replace("~", HttpRuntime.AppDomainAppVirtualPath);
					return new WebServiceReference(serviceUrl);
				}
				return base.ConvertFrom(context, culture, value);
			}

			// Token: 0x06004828 RID: 18472 RVA: 0x000DB6CB File Offset: 0x000D98CB
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06004829 RID: 18473 RVA: 0x000DB6EC File Offset: 0x000D98EC
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				WebServiceReference webServiceReference = value as WebServiceReference;
				if (destinationType == typeof(InstanceDescriptor) && webServiceReference != null)
				{
					ConstructorInfo constructor = typeof(WebServiceReference).GetConstructor(new Type[]
					{
						typeof(string)
					});
					return new InstanceDescriptor(constructor, new object[]
					{
						webServiceReference.ServiceUrl
					}, true);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x0600482A RID: 18474 RVA: 0x000DB760 File Offset: 0x000D9960
			internal static Type GetServiceClass(string serviceUrl)
			{
				string[] array = BuildManager.GetCompiledCustomString(serviceUrl).Split(WebServiceReference.WebServiceReferenceTypeConverter.compiledCustomStringSeparator, 4);
				string text = array[2];
				Type type = Type.GetType(text);
				if (type == null)
				{
					foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
					{
						type = assembly.GetType(text, false);
						if (type != null)
						{
							break;
						}
					}
					if (type == null)
					{
						throw new TypeLoadException(string.Format("Can't load class type '{0}' for service URL '{1}'.", text, serviceUrl));
					}
				}
				return type;
			}

			// Token: 0x04003072 RID: 12402
			private static char[] compiledCustomStringSeparator = new char[]
			{
				'|'
			};
		}
	}
}
