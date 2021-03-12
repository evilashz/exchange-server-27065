using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200011C RID: 284
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class DiagnosableArgument
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x0002145C File Offset: 0x0001F65C
		public DiagnosableArgument()
		{
			Dictionary<string, Type> dictionary = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);
			this.InitializeSchema(dictionary);
			this.ArgumentSchema = dictionary;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00021488 File Offset: 0x0001F688
		public int ArgumentCount
		{
			get
			{
				return this.Arguments.Count;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00021495 File Offset: 0x0001F695
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x0002149D File Offset: 0x0001F69D
		protected Dictionary<string, object> Arguments { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x000214A6 File Offset: 0x0001F6A6
		protected virtual bool FailOnMissingArgument
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x000214A9 File Offset: 0x0001F6A9
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x000214B1 File Offset: 0x0001F6B1
		private protected Dictionary<string, Type> ArgumentSchema { protected get; private set; }

		// Token: 0x0600084B RID: 2123 RVA: 0x000214BA File Offset: 0x0001F6BA
		public void Initialize(DiagnosableParameters parameters)
		{
			this.Initialize(parameters.Argument);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000214CC File Offset: 0x0001F6CC
		public void Initialize(string argument)
		{
			this.Arguments = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
			if (argument == null)
			{
				return;
			}
			string[] array = argument.Trim().Split(new string[]
			{
				","
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				string[] array3 = text.Split(new string[]
				{
					"="
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array3.Length > 0)
				{
					string text2 = array3[0].Trim();
					if (this.HasArgument(text2))
					{
						throw new ArgumentDuplicatedException(text2);
					}
					this.AddArgument(text2, (array3.Length > 1) ? array3[1].Trim() : null);
				}
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0002157C File Offset: 0x0001F77C
		public bool TryGetArgument<T>(string argumentToGet, out T val)
		{
			object obj;
			if (!this.HasArgument(argumentToGet) || (obj = this.Arguments[argumentToGet]) == null)
			{
				val = default(T);
				return false;
			}
			if (!(obj is T))
			{
				throw new ArgumentValueCannotBeParsedException(argumentToGet, obj.ToString(), typeof(T).Name);
			}
			val = (T)((object)obj);
			return true;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000215DC File Offset: 0x0001F7DC
		public T GetArgument<T>(string argumentToGet)
		{
			T result;
			this.TryGetArgument<T>(argumentToGet, out result);
			return result;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000215F4 File Offset: 0x0001F7F4
		public T GetArgumentOrDefault<T>(string argumentToGet, T defaultValue)
		{
			T result;
			if (this.TryGetArgument<T>(argumentToGet, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0002160F File Offset: 0x0001F80F
		public bool HasArgument(string argumentToCheck)
		{
			return this.Arguments.ContainsKey(argumentToCheck);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00021620 File Offset: 0x0001F820
		public virtual XElement RunDiagnosticOperation(Func<XElement> operation)
		{
			XElement result;
			try
			{
				result = operation();
			}
			catch (DiagnosticArgumentException ex)
			{
				result = new XElement("Error", "Encountered exception: " + ex.Message);
			}
			return result;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0002166C File Offset: 0x0001F86C
		public string GetSupportedArguments()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, Type> keyValuePair in this.ArgumentSchema)
			{
				if (keyValuePair.Value == typeof(bool))
				{
					list.Add(keyValuePair.Key);
				}
				else
				{
					list.Add(string.Format("{0}={1}", keyValuePair.Key, keyValuePair.Value.Name));
				}
			}
			return string.Join(", ", list);
		}

		// Token: 0x06000853 RID: 2131
		protected abstract void InitializeSchema(Dictionary<string, Type> schema);

		// Token: 0x06000854 RID: 2132 RVA: 0x00021714 File Offset: 0x0001F914
		protected virtual void AddArgument(string key, string value)
		{
			Type type;
			if (!this.ArgumentSchema.TryGetValue(key, out type))
			{
				if (this.FailOnMissingArgument)
				{
					throw new ArgumentNotSupportedException(key, this.GetSupportedArguments());
				}
				return;
			}
			else
			{
				if (value == null || type == typeof(bool))
				{
					this.Arguments.Add(key, null);
					return;
				}
				if (type == typeof(string))
				{
					this.Arguments.Add(key, value);
					return;
				}
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				if (converter == null)
				{
					throw new ArgumentValueCannotBeParsedException(key, value, type.Name);
				}
				try
				{
					this.Arguments.Add(key, converter.ConvertFromInvariantString(value));
				}
				catch (FormatException)
				{
					throw new ArgumentValueCannotBeParsedException(key, value, type.Name);
				}
				catch (NotSupportedException)
				{
					throw new ArgumentValueCannotBeParsedException(key, value, type.Name);
				}
				return;
			}
		}
	}
}
