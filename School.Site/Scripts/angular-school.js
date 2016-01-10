'use strict';

var schoolsApp = angular.module('schoolApp', []);

schoolsApp.factory('SchoolService', ['$http', '$filter', function ($http, $filter) {
    var School = function () {
        this.classes = [];
        this.students = [];
        this.schoolClassId = 0;
    };

    School.prototype.getClasses = function () {
        $http({ method: 'GET', url: '/api/schoolapi/GetSchoolClasses/' }).
        success(function (data) {
                this.classes = data;
            }.bind(this));
    };

    School.prototype.deleteClass = function (ClassId) {
        $http({ method: 'GET', url: '/api/schoolapi/DeleteClass/' + ClassId + '/' }).
             success(function (data) {
                 this.getClasses();
                 this.schoolClassId = 0;
             }.bind(this));
    };

    School.prototype.deleteStudent = function (StudentId) {
        $http({ method: 'GET', url: '/api/schoolapi/DeleteStudent/' + StudentId + '/' }).
             success(function (data) {
                 this.getStudentsByClass();
             }.bind(this));
    };

    School.prototype.getStudentsByClass = function () {
        $http({ method: 'GET', url: '/api/schoolapi/GetStudentsByClass/' + this.schoolClassId + '/' }).
             success(function (data) {
                 this.students = data;
             }.bind(this));
    };

    School.prototype.clear = function () {
        this.students = [];
        this.schoolClassId = 0;
    };

    return School;
}]);

var schoolController = schoolsApp.controller('schoolController', ['$scope', '$http', 'SchoolService', function ($scope, $http, SchoolService) {
    $scope.school = new SchoolService();
    $scope.selectedClass = 0;
    $scope.school.getClasses();
    $scope.selectedClassName = '';

    $scope.sort = {
        column: '',
        descending: false
    };

    // wait for click event on the class element
    $scope.getStudentsByClass = function (ClassId) {
        $scope.school.schoolClassId = ClassId;
        $scope.school.getStudentsByClass();
    }

    $scope.setSelected = function () {
        if ($scope.lastSelected) {
            $scope.lastSelected.selected = '';
        }
        this.selected = 'selected';
        $scope.lastSelected = this;
    }

    $scope.checkStudentVisibility = function () {
        if ($scope.selectedClass == 0)
            return true;
        return false;
    }

    $scope.setClass = function (ClassId, ClassName) {
        $scope.selectedClass = ClassId;
        $scope.selectedClassName = ClassName;
        $scope.getStudentsByClass(ClassId)
    }
    
    $scope.deleteClass = function (ClassId) {
        if (confirm('Are you sure you want to delete this class?')) {
            $scope.school.deleteClass(ClassId);
            $scope.selectedClass = 0;
        }
    }

    $scope.checkStudentGPA = function (GPA) {
        if (GPA > 3.2)
            return true;
        return false;
    }

    $scope.deleteStudent = function (StudentId) {
        if (confirm('Are you sure you want to delete this student?')) {
            $scope.school.deleteStudent(StudentId);
        }
    }

    $scope.changeSorting = function (column) {
        var sort = $scope.sort;
        sort.descending = !sort.descending;
        sort.column = column;
    };

    $scope.raiseNotImplementedException = function () {
        alert('Not implemented.');
    }
}]);