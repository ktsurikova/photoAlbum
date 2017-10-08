angular.
    module('imageGallery').
    component('photoList', {

        templateUrl: '/Photos/ShowImagesAngular',

        //template:
        //    //'<div ng-if="$ctrl.photosT()">' +
        //    //'<div ng-repeat="photo in $ctrl.photosT">likil</div>',
        //'<div class="container container-body images">' +
        //    '<div ng-repeat="photo in $ctrl.photos">' +
        //    '<div class="col-lg-4 col-md-6 col-sm-12 col-12 image">' +

        //    '<a id="fullsize" href="{{photo.ImageUrl}}" data-type="image" data-fancybox="images">' +
        //    '<img id="img" src="{{photo.ImageUrl}}" class="viewImage" />' +
        //    '</a>' +

        //    '</div>' +
        //    '</div>' +
        //    '</div>',
        ////'</div>',

        controller: function PhotoListController($http) {

            var data = {
                tag: '#steven',
                page: 1
            }

            var config = {
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;',
                    'ajax': true
                }
            }

            //var photos;

            var pt = this;

            $http.post('/Photos/LoadMore', data, config)
                .then(function (response) {
                    pt.photos = response.data.Items;
                });

            //this.photosT = function ($http) {
            //    return $http.post('/Photos/LoadMore', data, config)
            //        .then(function (response) {
            //            return response.data.Items;
            //        });
            //}

            //this.photosT = photos;
        }
    });