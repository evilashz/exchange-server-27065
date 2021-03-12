using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x0200007B RID: 123
	public sealed class CommandContext
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000681F File Offset: 0x00004A1F
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00006827 File Offset: 0x00004A27
		public string[] Expand { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00006830 File Offset: 0x00004A30
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00006838 File Offset: 0x00004A38
		public string IfMatchETag { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00006841 File Offset: 0x00004A41
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00006849 File Offset: 0x00004A49
		public int PageSizeOnReread
		{
			get
			{
				return this.pageSizeOnReread;
			}
			set
			{
				this.pageSizeOnReread = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00006852 File Offset: 0x00004A52
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000685A File Offset: 0x00004A5A
		public IEnumerable<PropertyDefinition> RequestedProperties { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00006864 File Offset: 0x00004A64
		private Dictionary<string, object> CustomParameters
		{
			get
			{
				Dictionary<string, object> result;
				if ((result = this.customParameters) == null)
				{
					result = (this.customParameters = new Dictionary<string, object>());
				}
				return result;
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00006889 File Offset: 0x00004A89
		public void SetCustomParameter(string parameter, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.CustomParameters.Add(parameter, value);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000068A8 File Offset: 0x00004AA8
		public bool TryGetCustomParameter<TValue>(string parameter, out TValue value)
		{
			object obj;
			if (!this.CustomParameters.TryGetValue(parameter, out obj))
			{
				value = default(TValue);
				return false;
			}
			Type type = obj.GetType();
			Type typeFromHandle = typeof(TValue);
			if (typeFromHandle.IsAssignableFrom(type))
			{
				value = (TValue)((object)obj);
				return true;
			}
			throw new InvalidCastException(string.Format("Cannot cast the stored value (Type: {0}) for the given parameter ('{1}') to type {2}", type.FullName, parameter, typeFromHandle.FullName));
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00006930 File Offset: 0x00004B30
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IfMatchETag != null)
			{
				stringBuilder.AppendFormat("[If-Match:{0}]", this.IfMatchETag);
			}
			if (this.Expand != null)
			{
				stringBuilder.AppendFormat("[Expand:{0}]", string.Join(",", this.Expand));
			}
			IEnumerable<string> values = from param in this.CustomParameters
			select string.Format("[X-{0}:{1}]", param.Key, param.Value);
			stringBuilder.Append(string.Join(",", values));
			return stringBuilder.ToString();
		}

		// Token: 0x040001A4 RID: 420
		internal const int DefaultPageSizeOnReread = 20;

		// Token: 0x040001A5 RID: 421
		private Dictionary<string, object> customParameters;

		// Token: 0x040001A6 RID: 422
		private int pageSizeOnReread = 20;
	}
}
