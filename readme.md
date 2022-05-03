# Dashboard for ASP.NET Core - How to refresh a specific dashboard item using parameters without refreshing the entire dashboard

The example shows how to refresh a specific dashboard item using parameters without refreshing the entire dashboard. This technique is useful when you have a requirement to filter a dashboard's data source using parameters and you do not want to refresh all items in the dashboard for performance reasons.

## Example Structure

The dashboard in the example has three items: 
- The "Products grid;
- The "Sales per Product" chart;
- The "Orders" grid.

![](Images/dashboard.png)

Each dashboard item displays data from its own data source. The "Products" grid has enabled [Master Filtering](https://docs.devexpress.com/Dashboard/117060/web-dashboard/create-dashboards-on-the-web/interactivity/master-filtering) to filter data in the "Sales per Product" chart. Since dashboard items have a different data source, the filtering is implemented using [Dashboard Parameters](https://docs.devexpress.com/Dashboard/117062/web-dashboard/create-dashboards-on-the-web/data-analysis/dashboard-parameters).

When the parameter value is changed in the dashboard, Web Dashboard refreshes all dashboard items to reflect possible changes in data sources. However, in this particular case, it is necessary to refresh only a single dashboard item. Thus, the parameter value is passed to the server using [Request Headers](https://docs.devexpress.com/Dashboard/js-DevExpress.Dashboard.AjaxRemoteService#js_devexpress_dashboard_ajaxremoteservice_headers) and the item is refreshed on the client side by calling the [DashboardControl.refresh](https://docs.devexpress.com/Dashboard/js-DevExpress.Dashboard.DashboardControl?p=netframework#js_devexpress_dashboard_dashboardcontrol_refresh) method.

```js
		function onBeforeRender(dashboardControl) {
			//...
			var viewerApi = dashboardControl.findExtension('viewerApi');
			if (viewerApi) {
				viewerApi.on('itemMasterFilterStateChanged', e => onItemMasterFilterStateChanged(dashboardControl, e));
			}
		}

		function onItemMasterFilterStateChanged(dashboardControl, e) {
			if (e.itemName === 'gridDashboardItem1') {
				var viewerApi = dashboardControl.findExtension('viewerApi');
				var filterValues = viewerApi.getCurrentFilterValues(e.itemName);
				if (filterValues) {
					var itemData = viewerApi.getItemData(e.itemName);
					var axisPoint = filterValues[0].getAxisPoint();
					var slice = itemData.getSlice(axisPoint);
					var productIdMeasure = slice.getMeasures().filter( m => m.dataMember === 'ProductID')[0];
					var productId = slice.getMeasureValue(productIdMeasure.id).getValue();

					// Send a ProductId value to the server using Request Headers
					dashboardControl.remoteService.headers = { 'ProductId' : productId};
					// Refresh the Chart Dashboard Item
					dashboardControl.refresh(['chartDashboardItem1']);
				}
			}
		}
```

```cs
        //...
        var contextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
        configurator.CustomParameters += (s, e) => {
            var value = Convert.ToInt32(contextAccessor?.HttpContext?.Request.Headers["ProductId"].FirstOrDefault());
            e.Parameters.Add(new DashboardParameter("ProductIdParameter", typeof(int), value));
        };
        //...
```

<!-- default file list -->
*Files to look at*:

* [Index.cshtml](./CS/AspNetCoreDashboard/Views/Home/Index.cshtml)
* [_Layout.cshtml](./CS/AspNetCoreDashboard/Views/Home/_Layout.cshtml)
* [DashboardUtils.cs](./CS/AspNetCoreDashboard/Code/DashboardUtils.cs)
<!-- default file list end -->

## Documentation

[Master Filtering](https://docs.devexpress.com/Dashboard/117060/web-dashboard/create-dashboards-on-the-web/interactivity/master-filtering)
[Manage an In-Memory Data Cache](https://docs.devexpress.com/Dashboard/400983/web-dashboard/dashboard-backend/manage-an-in-memory-data-cache)
