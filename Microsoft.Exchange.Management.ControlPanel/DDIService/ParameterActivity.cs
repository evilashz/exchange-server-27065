using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Markup;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000102 RID: 258
	[ContentProperty("Parameters")]
	public abstract class ParameterActivity : Activity
	{
		// Token: 0x06001F47 RID: 8007 RVA: 0x0005E0BB File Offset: 0x0005C2BB
		public ParameterActivity() : base(string.Empty)
		{
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x0005E0D3 File Offset: 0x0005C2D3
		public ParameterActivity(string name) : base(name)
		{
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x0005E0F4 File Offset: 0x0005C2F4
		protected ParameterActivity(ParameterActivity activity) : base(activity)
		{
			this.Parameters = new Collection<Parameter>((from c in activity.Parameters
			select c.Clone() as Parameter).ToList<Parameter>());
		}

		// Token: 0x17001A05 RID: 6661
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x0005E14B File Offset: 0x0005C34B
		// (set) Token: 0x06001F4B RID: 8011 RVA: 0x0005E153 File Offset: 0x0005C353
		[DDINoDuplication(PropertyName = "Name")]
		public Collection<Parameter> Parameters
		{
			get
			{
				return this.parameters;
			}
			set
			{
				this.parameters = value;
			}
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x0005E15C File Offset: 0x0005C35C
		public virtual IList<Parameter> GetEffectiveParameters(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			IList<Parameter> list = new List<Parameter>();
			foreach (Parameter item in this.Parameters)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x04001C61 RID: 7265
		private Collection<Parameter> parameters = new Collection<Parameter>();
	}
}
