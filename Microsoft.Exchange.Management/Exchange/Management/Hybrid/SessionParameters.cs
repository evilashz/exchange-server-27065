using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008FC RID: 2300
	internal class SessionParameters
	{
		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x06005187 RID: 20871 RVA: 0x001525D2 File Offset: 0x001507D2
		public int Count
		{
			get
			{
				return this.dictionary.Count<KeyValuePair<string, SessionParameters.SessionParameter>>();
			}
		}

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x06005188 RID: 20872 RVA: 0x001525ED File Offset: 0x001507ED
		public IEnumerable<string> LoggingText
		{
			get
			{
				return from kvp in this.dictionary
				select kvp.Value.LoggingText;
			}
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x00152618 File Offset: 0x00150818
		public IDictionary ToDictionary()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (SessionParameters.SessionParameter sessionParameter in this.dictionary.Values)
			{
				dictionary[sessionParameter.Name] = sessionParameter.Value;
			}
			return dictionary;
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x00152684 File Offset: 0x00150884
		public void Set(string name, string value)
		{
			this.SetValue<string>(name, value);
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x0015268E File Offset: 0x0015088E
		public void Set(string name, bool value)
		{
			this.SetValue<bool>(name, value);
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x00152698 File Offset: 0x00150898
		public void Set(string name, Guid value)
		{
			this.SetValue<Guid>(name, value);
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x001526A2 File Offset: 0x001508A2
		public void Set(string name, Uri value)
		{
			this.SetValue<Uri>(name, value);
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x001526AC File Offset: 0x001508AC
		public void Set(string name, Enum value)
		{
			this.SetValue<Enum>(name, value);
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x001526B8 File Offset: 0x001508B8
		public void SetNull<T>(string name)
		{
			this.SetValue<T>(name, default(T));
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x001526D5 File Offset: 0x001508D5
		public void Set<T>(string name, IEnumerable<T> list, Func<T, string> projection)
		{
			if (list != null && list.Count<T>() > 0)
			{
				this.SetValue<string[]>(name, list.Select(projection).ToArray<string>());
			}
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x00152705 File Offset: 0x00150905
		public void Set<T>(string name, IEnumerable<T> list)
		{
			this.Set<T>(name, list, (T x) => x.ToString());
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x0015271B File Offset: 0x0015091B
		public override string ToString()
		{
			return string.Join(" ", this.LoggingText);
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x0015272D File Offset: 0x0015092D
		private void SetValue<T>(string name, T value)
		{
			this.dictionary[name] = new SessionParameters.SessionParameter(name, typeof(T), value);
		}

		// Token: 0x04002FC7 RID: 12231
		private Dictionary<string, SessionParameters.SessionParameter> dictionary = new Dictionary<string, SessionParameters.SessionParameter>();

		// Token: 0x020008FD RID: 2301
		private class SessionParameter
		{
			// Token: 0x06005197 RID: 20887 RVA: 0x00152764 File Offset: 0x00150964
			public SessionParameter(string name, Type type, object value)
			{
				this.Name = name;
				this.Type = type;
				this.Value = value;
			}

			// Token: 0x17001879 RID: 6265
			// (get) Token: 0x06005198 RID: 20888 RVA: 0x00152781 File Offset: 0x00150981
			// (set) Token: 0x06005199 RID: 20889 RVA: 0x00152789 File Offset: 0x00150989
			public string Name { get; private set; }

			// Token: 0x1700187A RID: 6266
			// (get) Token: 0x0600519A RID: 20890 RVA: 0x00152792 File Offset: 0x00150992
			// (set) Token: 0x0600519B RID: 20891 RVA: 0x0015279A File Offset: 0x0015099A
			public object Value { get; private set; }

			// Token: 0x1700187B RID: 6267
			// (get) Token: 0x0600519C RID: 20892 RVA: 0x001527A3 File Offset: 0x001509A3
			// (set) Token: 0x0600519D RID: 20893 RVA: 0x001527AB File Offset: 0x001509AB
			public Type Type { get; private set; }

			// Token: 0x1700187C RID: 6268
			// (get) Token: 0x0600519E RID: 20894 RVA: 0x001527B4 File Offset: 0x001509B4
			public string LoggingText
			{
				get
				{
					if (this.Type == typeof(bool))
					{
						return string.Format("-{0}: {1}", this.Name, this.ValueLoggingText);
					}
					return string.Format("-{0} {1}", this.Name, this.ValueLoggingText);
				}
			}

			// Token: 0x1700187D RID: 6269
			// (get) Token: 0x0600519F RID: 20895 RVA: 0x00152808 File Offset: 0x00150A08
			private string ValueLoggingText
			{
				get
				{
					if (this.Type == typeof(bool))
					{
						if (!(bool)this.Value)
						{
							return "$false";
						}
						return "$true";
					}
					else
					{
						if (this.Value == null)
						{
							return "$null";
						}
						if (this.Type.IsArray)
						{
							return "{" + string.Join<object>(",", this.Value as IEnumerable<object>) + "}";
						}
						return string.Format("'{0}'", this.Value);
					}
				}
			}

			// Token: 0x060051A0 RID: 20896 RVA: 0x00152895 File Offset: 0x00150A95
			public override string ToString()
			{
				return this.LoggingText;
			}
		}
	}
}
