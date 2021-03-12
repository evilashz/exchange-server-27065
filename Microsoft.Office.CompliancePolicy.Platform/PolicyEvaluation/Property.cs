using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000D9 RID: 217
	public class Property : Argument
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x00010B89 File Offset: 0x0000ED89
		public Property(string propertyName, Type type) : base(type)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new CompliancePolicyValidationException("Property name must not be empty");
			}
			this.Name = propertyName;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00010BAC File Offset: 0x0000EDAC
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x00010BB4 File Offset: 0x0000EDB4
		public string Name { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00010BBD File Offset: 0x0000EDBD
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x00010BC5 File Offset: 0x0000EDC5
		public string SupplementalInfo { get; set; }

		// Token: 0x06000588 RID: 1416 RVA: 0x00010BD0 File Offset: 0x0000EDD0
		public static Property CreateProperty(string propertyName, string typeName)
		{
			if (string.IsNullOrWhiteSpace(typeName))
			{
				return new Property(propertyName, typeof(string));
			}
			try
			{
				return new Property(propertyName, Type.GetType(typeName));
			}
			catch (TargetInvocationException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (TypeLoadException)
			{
			}
			catch (FileLoadException)
			{
			}
			catch (BadImageFormatException)
			{
			}
			return null;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00010C58 File Offset: 0x0000EE58
		public override object GetValue(PolicyEvaluationContext context)
		{
			object obj = this.OnGetValue(context);
			if (obj != null)
			{
				Type type = obj.GetType();
				if (type != base.Type && !base.Type.IsAssignableFrom(type))
				{
					if (context.Tracer != null)
					{
						context.Tracer.TraceError("Property value is of the wrong type: {0}", new object[]
						{
							type
						});
					}
					return null;
				}
			}
			return obj;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00010CBC File Offset: 0x0000EEBC
		protected virtual object OnGetValue(PolicyEvaluationContext context)
		{
			string name;
			switch (name = this.Name)
			{
			case "Item.CreationAgeInDays":
				return (int)(DateTime.UtcNow - context.SourceItem.WhenCreated).TotalDays;
			case "Item.Extension":
				return context.SourceItem.Extension;
			case "Item.DisplayName":
				return context.SourceItem.DisplayName;
			case "Item.WhenCreated":
				return context.SourceItem.WhenCreated;
			case "Item.WhenModified":
				return context.SourceItem.WhenLastModified;
			case "Item.Creator":
				return context.SourceItem.Creator;
			case "Item.LastModifier":
				return context.SourceItem.LastModifier;
			case "Item.ExpiryTime":
				return context.SourceItem.ExpiryTime;
			case "Item.ClassificationDiscovered":
				return context.SourceItem.ClassificationDiscovered;
			case "Item.AccessScope":
				return context.SourceItem.AccessScope;
			case "Item.Metadata":
				return context.SourceItem.Metadata;
			}
			return null;
		}

		// Token: 0x020000DA RID: 218
		public static class PropertyNames
		{
			// Token: 0x0400033D RID: 829
			public const string Extension = "Item.Extension";

			// Token: 0x0400033E RID: 830
			public const string DisplayName = "Item.DisplayName";

			// Token: 0x0400033F RID: 831
			public const string WhenCreated = "Item.WhenCreated";

			// Token: 0x04000340 RID: 832
			public const string WhenModified = "Item.WhenModified";

			// Token: 0x04000341 RID: 833
			public const string Creator = "Item.Creator";

			// Token: 0x04000342 RID: 834
			public const string LastModifier = "Item.LastModifier";

			// Token: 0x04000343 RID: 835
			public const string ExpiryTime = "Item.ExpiryTime";

			// Token: 0x04000344 RID: 836
			public const string CreationAgeInDays = "Item.CreationAgeInDays";

			// Token: 0x04000345 RID: 837
			public const string CreationAgeInMonths = "Item.CreationAgeInMonths";

			// Token: 0x04000346 RID: 838
			public const string CreationAgeInYears = "Item.CreationAgeInYears";

			// Token: 0x04000347 RID: 839
			public const string ModificationAgeInDays = "Item.ModificationAgeInDays";

			// Token: 0x04000348 RID: 840
			public const string ModificationAgeInMonths = "Item.ModificationAgeInMonths";

			// Token: 0x04000349 RID: 841
			public const string ModificationAgeInYears = "Item.ModificationAgeInYears";

			// Token: 0x0400034A RID: 842
			public const string ClassificationDiscovered = "Item.ClassificationDiscovered";

			// Token: 0x0400034B RID: 843
			public const string AccessScope = "Item.AccessScope";

			// Token: 0x0400034C RID: 844
			public const string Metadata = "Item.Metadata";
		}
	}
}
