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



        changeModel: function (data) {

            this.model = data

            var x = this.model

        },


    },




})