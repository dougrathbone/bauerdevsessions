var giftRequest = angular.module("giftRequest", []);

giftRequest.controller('GiftRequestController', ["$scope", "GifeRequestService", function($scope, giftRequestService) {

    $scope.gifts = [];
    giftRequestService.getListOfGifts().then(function(result) {
        $scope.gifts = result;
    });

    $scope.submitGiftRequest = function() {
        $scope.selectedGift = { GiftCode: $scope.gift.Code, GiftName: $scope.gift.Name, RecipientFirstName: $scope.firstName, RecipientLastName: $scope.lastName };

        alert($scope.selectedGift.RecipientFirstName + " request for " + $scope.selectedGift.GiftName + " received!!!");
    };
    
}]);

giftRequest.service("GifeRequestService", ["$http", function ($http) {
    
    this.getListOfGifts = function () {
        
        return $http.get("/Default/GetListOfGifts").then(function(result) {
            return result.data;
        });
    };
    
}]);
