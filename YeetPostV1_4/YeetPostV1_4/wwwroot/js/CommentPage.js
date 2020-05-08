new Vue({
    el: '#app',
    vuetify: new Vuetify(),
    data() {
        const defaultForm = Object.freeze({
            terms: false,
        })

        return {
            header: '',
            dialog: false,
            model: model,
            addComment: '',
            abusive: 'abusive',
            inappropriate: 'innapropriate',
            select: '',
            yeet: '',
           
        }
    },

   

    methods: {
        resetForm() {
            this.form = Object.assign({}, this.defaultForm)
            this.$refs.form.reset()
        },
        submit() {

            this.resetForm()
            
            if ($("#comment").val() === null ) {
                return;
            }
            var e = this;
            var y = this.addComment;



            $.ajax({
                type: 'POST',
                url: '/Comment/addComment',
                data: {
                    comment: $("#comment").val(),
                    yeetId: e.model.yeet.yeetID
                },
                success: function (data) {
                   
                },
                error: function (data) {
                    console.log('Error, please report to a developer')
                }
            }).done(data => {
                this.model = JSON.parse(data);

            });

            //location.reload(true);
        },

        likeYeet: function (yeetID, whoLikes, location, remove) {
       
            
            $.ajax({
                type: 'GET',
                url: '/Like/LikePost',
                traditional: true,
                data: {
                    yeetID: yeetID,
                    whoLikes: whoLikes,
                    location: location,
                    status: status,
                    remove: remove,
                },
                success: function (data) {

                },
                error: function (data) {
                    console.log('Error, please report to a developer')
                }
            }).done(data => {


                var x = JSON.parse(data);
                this.model.yeet.iLiked = x.iLiked;
                this.model.yeet.totalLikes = x.totalLikes;
                this.model.yeet.whoLikes = x.whoLikes;

            });

        },
        //future put this in an object.
        flagYeet: function (yeetID, whoFlags, remove, reason, location) {
            if (reason !== "") {
                this.model.yeet.iFlagged = true;
            }
            else {
                this.model.yeet.iFlagged = false;
            }
            
            $.ajax({
                type: 'GET',
                url: '/Flag/FlagPost',
                traditional: true,
                data: {
                    yeetID: yeetID,
                    whoFlags: whoFlags,
                    location: location,
                    reason: reason,
                    remove: remove,
                    status: status,
                },
                success: function (data) {

                },
                error: function (data) {
                    console.log('Error, please report to a developer')
                }
            }).done(data => {
                //this.model = JSON.parse(data);
                this.model.yeet.modal = false;

             
            });


        },

        deleteYeet: function (yeetID) {

            $.ajax({
                type: 'POST',
                url: '/Comment/deleteYeet',
                traditional: true,
                data: {
                    yeetID: yeetID,
                  

                },
                success: function (data) {

                },
                error: function (data) {
                    console.log('Error, please report to a developer')
                }
            }).done(data => {
                this.model = JSON.parse(data);
            });
            return;


        },



        changeModel: function (data) {

            this.model = data

            var x = this.model

        },


    },




})