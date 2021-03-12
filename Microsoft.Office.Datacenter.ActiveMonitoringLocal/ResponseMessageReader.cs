using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000056 RID: 86
	internal class ResponseMessageReader
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x000165CC File Offset: 0x000147CC
		public void AddObject<T>(string objectName, T @object)
		{
			this.AddObjectResolver<T>(objectName, () => @object);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00016624 File Offset: 0x00014824
		public void AddObjectResolver<T>(string objectName, Func<T> objectResolver)
		{
			this.objectNameToLazilyResolvedInstance[objectName] = new Lazy<ResponseMessageReader.ReplaceableObject>(() => new ResponseMessageReader.ReplaceableObject(objectResolver(), new Func<object, string, object>(ResponseMessageReader.Schema<T>.GetValue)));
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001665C File Offset: 0x0001485C
		public object GetObject(string objectName)
		{
			return this.GetReplaceableObject(objectName).InnerObject;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000166A2 File Offset: 0x000148A2
		public string ReplaceValues(string replacementString)
		{
			return this.replacementExpression.Replace(replacementString, (Match match) => this.GetValue(match.Groups[1].Value, match.Groups[3].Value));
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000166BC File Offset: 0x000148BC
		private ResponseMessageReader.ReplaceableObject GetReplaceableObject(string objectName)
		{
			Lazy<ResponseMessageReader.ReplaceableObject> lazy;
			if (this.objectNameToLazilyResolvedInstance.TryGetValue(objectName, out lazy))
			{
				return lazy.Value;
			}
			throw new ArgumentException(string.Format("Object resolver is not registered for name '{0}'", objectName));
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000166F0 File Offset: 0x000148F0
		private string GetValue(string objectName, string propertyOrFieldName)
		{
			object member = this.GetReplaceableObject(objectName).GetMember(propertyOrFieldName);
			if (member == null)
			{
				return string.Empty;
			}
			return member.ToString();
		}

		// Token: 0x040003BF RID: 959
		private readonly Dictionary<string, Lazy<ResponseMessageReader.ReplaceableObject>> objectNameToLazilyResolvedInstance = new Dictionary<string, Lazy<ResponseMessageReader.ReplaceableObject>>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x040003C0 RID: 960
		private readonly Regex replacementExpression = new Regex("\\{([a-zA-Z0-9_]*)(\\.([a-zA-Z0-9_]+))?\\}", RegexOptions.Compiled);

		// Token: 0x02000057 RID: 87
		private struct ReplaceableObject
		{
			// Token: 0x0600057F RID: 1407 RVA: 0x00016746 File Offset: 0x00014946
			public ReplaceableObject(object innerObject, Func<object, string, object> getter)
			{
				this = default(ResponseMessageReader.ReplaceableObject);
				this.getter = getter;
				this.InnerObject = innerObject;
			}

			// Token: 0x170001CC RID: 460
			// (get) Token: 0x06000580 RID: 1408 RVA: 0x0001675D File Offset: 0x0001495D
			// (set) Token: 0x06000581 RID: 1409 RVA: 0x00016765 File Offset: 0x00014965
			public object InnerObject { get; private set; }

			// Token: 0x06000582 RID: 1410 RVA: 0x0001676E File Offset: 0x0001496E
			public object GetMember(string name)
			{
				return this.getter(this.InnerObject, name);
			}

			// Token: 0x040003C1 RID: 961
			private readonly Func<object, string, object> getter;
		}

		// Token: 0x02000058 RID: 88
		private static class Schema<T>
		{
			// Token: 0x06000583 RID: 1411 RVA: 0x00016784 File Offset: 0x00014984
			public static object GetValue(object inputObject, string propertyOrFieldName)
			{
				Func<object, object> func;
				if (!ResponseMessageReader.Schema<T>.propertyAndFieldGetters.TryGetValue(propertyOrFieldName, out func))
				{
					throw new ArgumentException(string.Format("Objects of type {0} don't have a field or property {1}", typeof(T), propertyOrFieldName));
				}
				if (inputObject == null)
				{
					return string.Empty;
				}
				return func(inputObject);
			}

			// Token: 0x06000584 RID: 1412 RVA: 0x00016800 File Offset: 0x00014A00
			private static Dictionary<string, Func<object, object>> GetPropertiesAndFieldsGetters(Type objectType)
			{
				Dictionary<string, Func<object, object>> dictionary = new Dictionary<string, Func<object, object>>();
				PropertyInfo[] properties = objectType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
				for (int i = 0; i < properties.Length; i++)
				{
					PropertyInfo property = properties[i];
					dictionary[property.Name] = ((object subject) => property.GetValue(subject, null));
				}
				FieldInfo[] fields = objectType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
				for (int j = 0; j < fields.Length; j++)
				{
					FieldInfo field = fields[j];
					dictionary[field.Name] = ((object subject) => field.GetValue(subject));
				}
				dictionary.Add(string.Empty, (object subject) => subject.ToString());
				return dictionary;
			}

			// Token: 0x040003C3 RID: 963
			private static readonly Dictionary<string, Func<object, object>> propertyAndFieldGetters = ResponseMessageReader.Schema<T>.GetPropertiesAndFieldsGetters(typeof(T));
		}
	}
}
