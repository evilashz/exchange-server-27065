using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001CD RID: 461
	internal abstract class UMPartnerContext
	{
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001030 RID: 4144
		protected abstract UMPartnerContext.UMPartnerContextSchema ContextSchema { get; }

		// Token: 0x170004FB RID: 1275
		protected object this[UMPartnerContext.UMPartnerContextPropertyDefinition definition]
		{
			get
			{
				return this.propertyBag[definition.Name];
			}
			set
			{
				if (value != null)
				{
					this.propertyBag[definition.Name] = value;
					return;
				}
				this.propertyBag[definition.Name] = definition.DefaultValue;
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x000314D0 File Offset: 0x0002F6D0
		public static T Create<T>() where T : UMPartnerContext, new()
		{
			T t = Activator.CreateInstance<T>();
			List<UMPartnerContext.UMPartnerContextPropertyDefinition> propertyDefinitions = t.GetPropertyDefinitions();
			foreach (UMPartnerContext.UMPartnerContextPropertyDefinition umpartnerContextPropertyDefinition in propertyDefinitions)
			{
				t.propertyBag[umpartnerContextPropertyDefinition.Name] = umpartnerContextPropertyDefinition.DefaultValue;
			}
			return t;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00031548 File Offset: 0x0002F748
		public static T Parse<T>(string base64Data) where T : UMPartnerContext, new()
		{
			T result;
			if (!UMPartnerContext.TryParse<T>(base64Data, out result))
			{
				throw new ArgumentException("base64Data");
			}
			return result;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0003156C File Offset: 0x0002F76C
		public static bool TryParse<T>(string base64Data, out T partnerContext) where T : UMPartnerContext, new()
		{
			partnerContext = default(T);
			try
			{
				if (!string.IsNullOrEmpty(base64Data))
				{
					string @string = Encoding.UTF8.GetString(Convert.FromBase64String(base64Data));
					NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(@string);
					if (nameValueCollection != null)
					{
						partnerContext = Activator.CreateInstance<T>();
						partnerContext.Initialize(nameValueCollection);
					}
				}
			}
			catch (FormatException)
			{
				partnerContext = default(T);
			}
			catch (ArgumentException)
			{
				partnerContext = default(T);
			}
			return partnerContext != null;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00031600 File Offset: 0x0002F800
		public override string ToString()
		{
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(string.Empty);
			foreach (object obj in this.propertyBag.Keys)
			{
				string text = (string)obj;
				object obj2 = this.propertyBag[text];
				if (obj2 != null)
				{
					nameValueCollection[text] = ValueConvertor.ConvertValueToString(obj2, null);
				}
			}
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(nameValueCollection.ToString()));
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00031698 File Offset: 0x0002F898
		private void Initialize(NameValueCollection keyValuePairs)
		{
			List<UMPartnerContext.UMPartnerContextPropertyDefinition> propertyDefinitions = this.GetPropertyDefinitions();
			foreach (UMPartnerContext.UMPartnerContextPropertyDefinition umpartnerContextPropertyDefinition in propertyDefinitions)
			{
				string text = keyValuePairs[umpartnerContextPropertyDefinition.Name];
				if (text == null)
				{
					this.propertyBag[umpartnerContextPropertyDefinition.Name] = umpartnerContextPropertyDefinition.DefaultValue;
				}
				else
				{
					this.propertyBag[umpartnerContextPropertyDefinition.Name] = umpartnerContextPropertyDefinition.Validate(text);
				}
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00031728 File Offset: 0x0002F928
		private List<UMPartnerContext.UMPartnerContextPropertyDefinition> GetPropertyDefinitions()
		{
			FieldInfo[] fields = this.ContextSchema.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			List<UMPartnerContext.UMPartnerContextPropertyDefinition> list = new List<UMPartnerContext.UMPartnerContextPropertyDefinition>(fields.Length);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (typeof(UMPartnerContext.UMPartnerContextPropertyDefinition).Equals(fieldInfo.FieldType))
				{
					list.Add((UMPartnerContext.UMPartnerContextPropertyDefinition)fieldInfo.GetValue(this.ContextSchema));
				}
			}
			return list;
		}

		// Token: 0x040009A4 RID: 2468
		private Hashtable propertyBag = new Hashtable();

		// Token: 0x020001CE RID: 462
		protected class UMPartnerContextPropertyDefinition : PropertyDefinition
		{
			// Token: 0x06001039 RID: 4153 RVA: 0x00031799 File Offset: 0x0002F999
			public UMPartnerContextPropertyDefinition(string name, Type type, object defaultValue) : base(name, type)
			{
				this.DefaultValue = defaultValue;
			}

			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x0600103A RID: 4154 RVA: 0x000317AA File Offset: 0x0002F9AA
			// (set) Token: 0x0600103B RID: 4155 RVA: 0x000317B2 File Offset: 0x0002F9B2
			public object DefaultValue { get; private set; }

			// Token: 0x0600103C RID: 4156 RVA: 0x000317BC File Offset: 0x0002F9BC
			public object Validate(string propertyValue)
			{
				object result;
				Exception innerException;
				if (!ValueConvertor.TryConvertValueFromString(propertyValue, base.Type, null, out result, out innerException))
				{
					throw new ArgumentException("Invalid property", base.Name, innerException);
				}
				return result;
			}
		}

		// Token: 0x020001CF RID: 463
		protected abstract class UMPartnerContextSchema
		{
		}
	}
}
