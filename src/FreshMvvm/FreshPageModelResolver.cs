using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FreshMvvm
{
    public static class FreshPageModelResolver
    {
        public static async Task<Page> ResolvePageModel<T>() where T : FreshBasePageModel
        {
            return await ResolvePageModel<T> (null);
        }

        public static async Task<Page> ResolvePageModel<T> (object initData) where T : FreshBasePageModel
        {
            var pageModel = FreshIOC.Container.Resolve<T> ();

            return await ResolvePageModel<T> (initData, pageModel);
        }

        public static async Task<Page> ResolvePageModel<T> (object data, T pageModel) where T : FreshBasePageModel
        {
            var type = pageModel.GetType ();
            var name = type.AssemblyQualifiedName.Replace ("Model", string.Empty);
            var pageType = Type.GetType (name);
            if (pageType == null)
                throw new Exception (name + " not found");

            var page = (Page)FreshIOC.Container.Resolve (pageType);

            page.BindingContext = pageModel;
            pageModel.WireEvents (page);
            pageModel.CurrentPage = page;
			pageModel.CoreMethods = new PageModelCoreMethods (page, pageModel);
            await pageModel.Init (data);

            return page;
        }

    }
}

