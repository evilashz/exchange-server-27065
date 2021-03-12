using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x020003D8 RID: 984
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	internal sealed class RedactAttribute : Attribute
	{
		// Token: 0x06002317 RID: 8983 RVA: 0x0008E8F0 File Offset: 0x0008CAF0
		static RedactAttribute()
		{
			RedactAttribute.redactionFunctions.Add(typeof(string), delegate(PropertyInfo property, object propertyInstance)
			{
				RedactAttribute.Redact(property, propertyInstance, (string value) => SuppressingPiiData.Redact(value));
			});
			RedactAttribute.redactionFunctions.Add(typeof(SmtpAddress), delegate(PropertyInfo property, object propertyInstance)
			{
				RedactAttribute.Redact(property, propertyInstance, (string value) => SuppressingPiiData.RedactSmtpAddress(value));
			});
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x0008E969 File Offset: 0x0008CB69
		// (set) Token: 0x06002319 RID: 8985 RVA: 0x0008E971 File Offset: 0x0008CB71
		public Type RedactAs { get; set; }

		// Token: 0x0600231A RID: 8986 RVA: 0x0008E97C File Offset: 0x0008CB7C
		internal void Redact(PropertyInfo property, object propertyInstance)
		{
			Type type = (this.RedactAs != null) ? this.RedactAs : property.PropertyType;
			RedactAttribute.RedactionDelegate redactionDelegate;
			if (RedactAttribute.redactionFunctions.TryGetValue(type, out redactionDelegate))
			{
				redactionDelegate(property, propertyInstance);
				return;
			}
			throw new NotSupportedException(string.Format("Redaction is not supported for {0}.", type.Name));
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x0008E9D4 File Offset: 0x0008CBD4
		private static void Redact(PropertyInfo property, object propertyInstance, Func<string, string> redactionFunction)
		{
			string text = property.GetValue(propertyInstance) as string;
			if (text != null)
			{
				string value = redactionFunction(text);
				property.SetValue(propertyInstance, value);
			}
		}

		// Token: 0x04001BCC RID: 7116
		private static readonly Dictionary<Type, RedactAttribute.RedactionDelegate> redactionFunctions = new Dictionary<Type, RedactAttribute.RedactionDelegate>();

		// Token: 0x020003D9 RID: 985
		// (Invoke) Token: 0x06002322 RID: 8994
		private delegate void RedactionDelegate(PropertyInfo property, object propertyInstance);
	}
}
