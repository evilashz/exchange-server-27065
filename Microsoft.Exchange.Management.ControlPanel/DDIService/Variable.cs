using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000188 RID: 392
	[DDIAllVariableHasRoles]
	[DDIPropertyExistInDataObject]
	public class Variable : ICloneable
	{
		// Token: 0x0600226E RID: 8814 RVA: 0x000684DA File Offset: 0x000666DA
		public Variable()
		{
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000684E2 File Offset: 0x000666E2
		public Variable(string name, string dataObjectName, string mappingProperty)
		{
			this.name = name;
			this.dataObjectName = dataObjectName;
			this.mappingProperty = (string.IsNullOrEmpty(mappingProperty) ? name : mappingProperty);
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0006850C File Offset: 0x0006670C
		public object Clone()
		{
			return new Variable(this.Name, this.DataObjectName, this.MappingProperty)
			{
				PropertySetter = this.PropertySetter,
				PersistWholeObject = this.PersistWholeObject,
				IgnoreChangeTracking = this.IgnoreChangeTracking,
				UnicodeString = this.UnicodeString,
				Type = this.Type,
				Value = this.Value,
				OutputConverter = this.OutputConverter,
				InputConverter = this.InputConverter,
				SetRoles = this.SetRoles,
				NewRoles = this.NewRoles,
				RbacDataObjectName = this.RbacDataObjectName,
				RbacDependenciesForNew = this.RbacDependenciesForNew,
				RbacDependenciesForSet = this.RbacDependenciesForSet
			};
		}

		// Token: 0x17001A94 RID: 6804
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x000685CE File Offset: 0x000667CE
		// (set) Token: 0x06002272 RID: 8818 RVA: 0x000685D6 File Offset: 0x000667D6
		[DDIMandatoryValue]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17001A95 RID: 6805
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x000685DF File Offset: 0x000667DF
		// (set) Token: 0x06002274 RID: 8820 RVA: 0x000685E7 File Offset: 0x000667E7
		[DefaultValue(null)]
		public IPropertySetter PropertySetter
		{
			get
			{
				return this.propertySetter;
			}
			set
			{
				this.propertySetter = value;
			}
		}

		// Token: 0x17001A96 RID: 6806
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x000685F0 File Offset: 0x000667F0
		// (set) Token: 0x06002276 RID: 8822 RVA: 0x000685F8 File Offset: 0x000667F8
		[DefaultValue(false)]
		public bool PersistWholeObject { get; set; }

		// Token: 0x17001A97 RID: 6807
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x00068601 File Offset: 0x00066801
		// (set) Token: 0x06002278 RID: 8824 RVA: 0x00068609 File Offset: 0x00066809
		[DefaultValue(false)]
		public bool IgnoreChangeTracking { get; set; }

		// Token: 0x17001A98 RID: 6808
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x00068612 File Offset: 0x00066812
		// (set) Token: 0x0600227A RID: 8826 RVA: 0x0006861A File Offset: 0x0006681A
		[DefaultValue(false)]
		public bool UnicodeString { get; set; }

		// Token: 0x17001A99 RID: 6809
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x00068623 File Offset: 0x00066823
		// (set) Token: 0x0600227C RID: 8828 RVA: 0x0006862B File Offset: 0x0006682B
		[DefaultValue(null)]
		[TypeConverter(typeof(DDIObjectTypeConverter))]
		public Type Type { get; set; }

		// Token: 0x17001A9A RID: 6810
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x00068634 File Offset: 0x00066834
		// (set) Token: 0x0600227E RID: 8830 RVA: 0x0006863C File Offset: 0x0006683C
		[DefaultValue(null)]
		[DDIValidLambdaExpression]
		public object Value { get; set; }

		// Token: 0x17001A9B RID: 6811
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x00068645 File Offset: 0x00066845
		// (set) Token: 0x06002280 RID: 8832 RVA: 0x00068661 File Offset: 0x00066861
		public string MappingProperty
		{
			get
			{
				if (!string.IsNullOrEmpty(this.mappingProperty))
				{
					return this.mappingProperty;
				}
				return this.Name;
			}
			set
			{
				this.mappingProperty = value;
			}
		}

		// Token: 0x17001A9C RID: 6812
		// (get) Token: 0x06002281 RID: 8833 RVA: 0x0006866A File Offset: 0x0006686A
		// (set) Token: 0x06002282 RID: 8834 RVA: 0x00068672 File Offset: 0x00066872
		[DefaultValue(null)]
		[DDIDataObjectNameExist]
		public string DataObjectName
		{
			get
			{
				return this.dataObjectName;
			}
			set
			{
				this.dataObjectName = value;
			}
		}

		// Token: 0x17001A9D RID: 6813
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x0006867B File Offset: 0x0006687B
		// (set) Token: 0x06002284 RID: 8836 RVA: 0x00068683 File Offset: 0x00066883
		[DDIDataObjectNameExist]
		public string RbacDataObjectName { get; set; }

		// Token: 0x17001A9E RID: 6814
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x0006868C File Offset: 0x0006688C
		// (set) Token: 0x06002286 RID: 8838 RVA: 0x00068694 File Offset: 0x00066894
		[DefaultValue(null)]
		public IInputConverter InputConverter { get; set; }

		// Token: 0x17001A9F RID: 6815
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x0006869D File Offset: 0x0006689D
		// (set) Token: 0x06002288 RID: 8840 RVA: 0x000686A5 File Offset: 0x000668A5
		[DefaultValue(null)]
		public IOutputConverter OutputConverter { get; set; }

		// Token: 0x17001AA0 RID: 6816
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x000686AE File Offset: 0x000668AE
		// (set) Token: 0x0600228A RID: 8842 RVA: 0x000686B6 File Offset: 0x000668B6
		[DDIValidRole]
		public string SetRoles { get; set; }

		// Token: 0x17001AA1 RID: 6817
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x000686BF File Offset: 0x000668BF
		// (set) Token: 0x0600228C RID: 8844 RVA: 0x000686C7 File Offset: 0x000668C7
		[DDIValidRole]
		public string NewRoles { get; set; }

		// Token: 0x17001AA2 RID: 6818
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x000686D0 File Offset: 0x000668D0
		// (set) Token: 0x0600228E RID: 8846 RVA: 0x000686D8 File Offset: 0x000668D8
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] RbacDependenciesForNew { get; set; }

		// Token: 0x17001AA3 RID: 6819
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x000686E1 File Offset: 0x000668E1
		// (set) Token: 0x06002290 RID: 8848 RVA: 0x000686E9 File Offset: 0x000668E9
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] RbacDependenciesForSet { get; set; }

		// Token: 0x06002291 RID: 8849 RVA: 0x000686F2 File Offset: 0x000668F2
		public Variable ShallowClone()
		{
			return base.MemberwiseClone() as Variable;
		}

		// Token: 0x04001D76 RID: 7542
		public const string ReadOnlyVariableName = "IsReadOnly";

		// Token: 0x04001D77 RID: 7543
		internal static readonly string[] MandatoryVariablesForGetObject = new string[]
		{
			"IsReadOnly"
		};

		// Token: 0x04001D78 RID: 7544
		private string name;

		// Token: 0x04001D79 RID: 7545
		private string dataObjectName;

		// Token: 0x04001D7A RID: 7546
		private string mappingProperty;

		// Token: 0x04001D7B RID: 7547
		private IPropertySetter propertySetter;
	}
}
