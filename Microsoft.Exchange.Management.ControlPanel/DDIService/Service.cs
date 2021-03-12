using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Markup;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200015F RID: 351
	[ContentProperty("Workflows")]
	public class Service : ICloneable
	{
		// Token: 0x060021B9 RID: 8633 RVA: 0x000656C1 File Offset: 0x000638C1
		static Service()
		{
			ExpressionParser.RemovePredefinedTypes(typeof(Microsoft.Exchange.ManagementGUI.Resources.Strings));
			ExpressionParser.EnrolPredefinedTypes(typeof(Microsoft.Exchange.Management.ControlPanel.Strings));
			ExpressionParser.EnrolPredefinedTypes(typeof(DDIHelper));
			ExpressionParser.EnrolPredefinedTypes(typeof(DDIUtil));
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x00065724 File Offset: 0x00063924
		public object Clone()
		{
			Service service = new Service();
			service.Name = this.Name;
			service.LambdaExpressionHelper = this.LambdaExpressionHelper;
			service.Class = this.Class;
			service.Variables = new Collection<Variable>((from c in this.Variables
			select c.Clone() as Variable).ToList<Variable>());
			service.DataObjects = new Collection<DataObject>((from c in this.DataObjects
			select c.Clone() as DataObject).ToList<DataObject>());
			service.Workflows = new Collection<Workflow>((from c in this.Workflows
			select c.Clone()).ToList<Workflow>());
			service.Resources = new Dictionary<string, object>(this.Resources);
			return service;
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x00065815 File Offset: 0x00063A15
		public Service()
		{
			this.Resources = new Dictionary<string, object>();
		}

		// Token: 0x17001A7C RID: 6780
		// (get) Token: 0x060021BC RID: 8636 RVA: 0x00065849 File Offset: 0x00063A49
		// (set) Token: 0x060021BD RID: 8637 RVA: 0x00065851 File Offset: 0x00063A51
		public string Name { get; set; }

		// Token: 0x17001A7D RID: 6781
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x0006585A File Offset: 0x00063A5A
		// (set) Token: 0x060021BF RID: 8639 RVA: 0x00065862 File Offset: 0x00063A62
		public Type[] LambdaExpressionHelper
		{
			get
			{
				return this.lambdaExpressionHelper;
			}
			set
			{
				if (value != this.lambdaExpressionHelper)
				{
					this.lambdaExpressionHelper = value;
				}
			}
		}

		// Token: 0x17001A7E RID: 6782
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x00065874 File Offset: 0x00063A74
		// (set) Token: 0x060021C1 RID: 8641 RVA: 0x0006587C File Offset: 0x00063A7C
		[TypeConverter(typeof(DDIObjectTypeConverter))]
		public Type Class
		{
			get
			{
				return this.classType;
			}
			set
			{
				if (this.classType != value)
				{
					this.classType = value;
				}
			}
		}

		// Token: 0x17001A7F RID: 6783
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x00065893 File Offset: 0x00063A93
		// (set) Token: 0x060021C3 RID: 8643 RVA: 0x0006589B File Offset: 0x00063A9B
		[DDIMandatoryValue]
		[DDINoDuplication(PropertyName = "Name")]
		public Collection<Variable> Variables
		{
			get
			{
				return this.variables;
			}
			set
			{
				this.variables = value;
			}
		}

		// Token: 0x17001A80 RID: 6784
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x000658A4 File Offset: 0x00063AA4
		// (set) Token: 0x060021C5 RID: 8645 RVA: 0x000658AC File Offset: 0x00063AAC
		[DDINoDuplication(PropertyName = "Name")]
		public Collection<DataObject> DataObjects
		{
			get
			{
				return this.dataObjects;
			}
			set
			{
				this.dataObjects = value;
			}
		}

		// Token: 0x17001A81 RID: 6785
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x000658B5 File Offset: 0x00063AB5
		// (set) Token: 0x060021C7 RID: 8647 RVA: 0x000658BD File Offset: 0x00063ABD
		[DDIMandatoryValue]
		[DDINoDuplication(PropertyName = "Name")]
		public Collection<Workflow> Workflows
		{
			get
			{
				return this.workflows;
			}
			set
			{
				this.workflows = value;
			}
		}

		// Token: 0x17001A82 RID: 6786
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x000658C8 File Offset: 0x00063AC8
		internal List<Type> PredefinedTypes
		{
			get
			{
				if (this.predefinedTypes == null)
				{
					this.predefinedTypes = new List<Type>();
					if (this.LambdaExpressionHelper != null)
					{
						this.predefinedTypes.AddRange(this.LambdaExpressionHelper);
					}
					if (null != this.Class)
					{
						this.predefinedTypes.Add(this.Class);
					}
				}
				return this.predefinedTypes;
			}
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x00065958 File Offset: 0x00063B58
		public virtual void Initialize()
		{
			this.Variables.Add(new Variable
			{
				Name = "ShouldContinue",
				Type = typeof(bool)
			});
			this.Variables.Add(new Variable
			{
				Name = "LastErrorContext",
				Type = typeof(ErrorRecordContext)
			});
			List<Workflow> list = new List<Workflow>();
			IEnumerable<Workflow> source = from c in this.workflows
			where c is GetObjectForListWorkflow
			select c;
			Workflow workflow = source.FirstOrDefault<Workflow>();
			IEnumerable<Workflow> source2 = from c in this.workflows
			where c is GetObjectWorkflow
			select c;
			Workflow workflow2 = source2.FirstOrDefault<Workflow>();
			bool flag = source2.Count<Workflow>() > 1;
			list.AddRange(from c in this.workflows
			where c is ICallGetAfterExecuteWorkflow && !((ICallGetAfterExecuteWorkflow)c).IgnoreGetObject
			select c);
			if (workflow2 != null)
			{
				foreach (Workflow workflow3 in list)
				{
					List<Activity> list2 = new List<Activity>(workflow3.Activities);
					if (flag)
					{
						list2.AddRange(workflow.Activities);
					}
					else
					{
						list2.AddRange(workflow2.Activities);
					}
					workflow3.Activities = new Collection<Activity>(list2);
				}
			}
		}

		// Token: 0x17001A83 RID: 6787
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x00065AEC File Offset: 0x00063CEC
		public List<DataColumn> ExtendedColumns
		{
			get
			{
				List<DataColumn> list = new List<DataColumn>();
				foreach (Workflow workflow in this.workflows)
				{
					foreach (Activity activity in workflow.Activities)
					{
						list.AddRange(activity.GetExtendedColumns());
					}
				}
				return list;
			}
		}

		// Token: 0x17001A84 RID: 6788
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x00065B80 File Offset: 0x00063D80
		// (set) Token: 0x060021CC RID: 8652 RVA: 0x00065B88 File Offset: 0x00063D88
		public IDictionary<string, object> Resources { get; private set; }

		// Token: 0x04001D3E RID: 7486
		private Type[] lambdaExpressionHelper;

		// Token: 0x04001D3F RID: 7487
		private Collection<Variable> variables = new Collection<Variable>();

		// Token: 0x04001D40 RID: 7488
		private Collection<DataObject> dataObjects = new Collection<DataObject>();

		// Token: 0x04001D41 RID: 7489
		private Collection<Workflow> workflows = new Collection<Workflow>();

		// Token: 0x04001D42 RID: 7490
		private List<Type> predefinedTypes;

		// Token: 0x04001D43 RID: 7491
		private Type classType;
	}
}
