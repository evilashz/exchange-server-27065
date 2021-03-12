using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000471 RID: 1137
	public class SlabDefinition
	{
		// Token: 0x0600267E RID: 9854 RVA: 0x0008B4B2 File Offset: 0x000896B2
		public SlabDefinition()
		{
			this.types = new List<string>();
			this.templates = new List<string>();
			this.bindings = new List<SlabBinding>();
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x0008B4DB File Offset: 0x000896DB
		protected IList<SlabBinding> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x0008B4E3 File Offset: 0x000896E3
		protected IList<string> Templates
		{
			get
			{
				return this.templates;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x0008B4EB File Offset: 0x000896EB
		protected IList<string> Types
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x0008B4F3 File Offset: 0x000896F3
		public void AddType(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.types.Add(typeName);
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0008B50F File Offset: 0x0008970F
		public void AddTemplate(string templateName)
		{
			if (templateName == null)
			{
				throw new ArgumentNullException("templateName");
			}
			this.templates.Add(templateName);
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x0008B52B File Offset: 0x0008972B
		public string[] GetTypes()
		{
			return this.types.ToArray<string>();
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x0008B538 File Offset: 0x00089738
		public void AddBinding(SlabBinding binding)
		{
			if (binding == null)
			{
				throw new ArgumentNullException("binding");
			}
			this.bindings.Add(binding);
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0008B560 File Offset: 0x00089760
		public IEnumerable<string> GetFeatures()
		{
			return from binding in this.bindings
			from feature in binding.Features
			select feature;
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x0008B5B4 File Offset: 0x000897B4
		public bool HasBinding(string featureName)
		{
			if (featureName == null)
			{
				throw new ArgumentNullException("featureName");
			}
			return this.HasBinding(new string[]
			{
				featureName
			});
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x0008B5F8 File Offset: 0x000897F8
		public bool HasBinding(string[] featureNames)
		{
			if (featureNames == null)
			{
				throw new ArgumentNullException("featureNames");
			}
			return this.bindings.Any((SlabBinding b) => b.Implement(featureNames));
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x0008B644 File Offset: 0x00089844
		public bool HasDefaultBinding()
		{
			return this.bindings.Any((SlabBinding b) => b.IsDefault);
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x0008B670 File Offset: 0x00089870
		public Slab GetDefaultSlab(LayoutType layout, out IEnumerable<string> slabDependencies)
		{
			SlabBinding binding = this.FindDefaultBinding();
			return this.GetSlabForBinding(binding, layout, out slabDependencies);
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x0008B690 File Offset: 0x00089890
		public virtual Slab GetSlab(string[] featureNames, LayoutType layout, out IEnumerable<string> slabDependencies)
		{
			if (featureNames == null)
			{
				throw new ArgumentNullException("featureNames");
			}
			SlabBinding binding = this.FindBinding(featureNames);
			return this.GetSlabForBinding(binding, layout, out slabDependencies);
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x0008B734 File Offset: 0x00089934
		protected virtual Slab GetSlabForBinding(SlabBinding binding, LayoutType layout, out IEnumerable<string> slabDependencies)
		{
			if (binding == null)
			{
				throw new ArgumentNullException("binding");
			}
			slabDependencies = binding.Dependencies;
			return new Slab
			{
				Types = this.types.ToArray<string>(),
				Templates = this.templates.ToArray<string>(),
				Styles = (from s in binding.Styles
				where s.IsForLayout(layout)
				select s).ToArray<SlabStyleFile>(),
				Configurations = (from s in binding.Configurations.Distinct<SlabConfiguration>()
				where s.IsForLayout(layout)
				select s).ToArray<SlabConfiguration>(),
				Dependencies = binding.Dependencies.Distinct<string>().ToArray<string>(),
				PackagedSources = (from s in binding.PackagedSources
				where s.IsForLayout(layout)
				select s).ToArray<SlabSourceFile>(),
				Images = (from s in binding.Images
				where s.IsForLayout(layout)
				select s).ToArray<SlabImageFile>(),
				Fonts = (from s in binding.Fonts
				where s.IsForLayout(layout)
				select s).ToArray<SlabFontFile>(),
				Sources = (from s in binding.Sources
				where s.IsForLayout(layout)
				select s).ToArray<SlabSourceFile>(),
				Strings = (from s in binding.Strings
				where s.IsForLayout(layout)
				select s).ToArray<SlabStringFile>(),
				PackagedStrings = (from s in binding.PackagedStrings
				where s.IsForLayout(layout)
				select s).ToArray<SlabStringFile>()
			};
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x0008B8C4 File Offset: 0x00089AC4
		protected SlabBinding FindDefaultBinding()
		{
			SlabBinding slabBinding = this.bindings.FirstOrDefault((SlabBinding b) => b.IsDefault);
			if (slabBinding == null)
			{
				throw new SlabBindingNotFoundException("Default Binding not found");
			}
			return slabBinding;
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x0008B938 File Offset: 0x00089B38
		private SlabBinding FindBinding(string[] featureNames)
		{
			using (IEnumerator<SlabBinding> enumerator = this.Bindings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SlabBinding binding = enumerator.Current;
					if (featureNames.Any((string feature) => binding.Implement(new string[]
					{
						feature
					})))
					{
						return binding;
					}
				}
			}
			throw new SlabBindingNotFoundException(string.Format("Binding not found for feature '{0}'", string.Join(",", featureNames)));
		}

		// Token: 0x0400167F RID: 5759
		private readonly IList<string> types;

		// Token: 0x04001680 RID: 5760
		private readonly IList<string> templates;

		// Token: 0x04001681 RID: 5761
		private readonly IList<SlabBinding> bindings;
	}
}
